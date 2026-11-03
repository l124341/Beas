using System.Data.Odbc;
using BeasSwagger.Models;
using Microsoft.Extensions.Options;

namespace BeasSwagger.Services;

/// <summary>把 Beas Script 寫入 BEAS_COMMON_INPUT,再輪詢 BEAS_COMMON_OUTPUT 取結果。</summary>
public interface IBeasScriptClient
{
    // pre:(選填)與寫入 INPUT 同一個交易、先執行的 SQL(例如 routing.change 先刪舊途程);需 AllowDirectSqlWrite。
    Task<BeasResult> RunScriptAsync(string requestId, string script, BeasConnection? conn, CancellationToken ct = default, IReadOnlyList<BeasSqlStatement>? pre = null);
}

public class BeasScriptOptions
{
    /// <summary>ODBC 連線字串樣板,可用 {server} {database} {user} {password}。需與原 DLL 的 connection() 一致。</summary>
    public string OdbcConnectionString { get; set; } = "";
    public int PollIntervalSeconds { get; set; } = 5;
    public int MaxPolls { get; set; } = 10;
    /// <summary>是否允許直接對 Beas 資料表寫入 SQL(routing/delete、routing/change)。預設 false;Beas 文件不建議直接改 Beas 表。</summary>
    public bool AllowDirectSqlWrite { get; set; } = false;
}

public class MockBeasScriptClient(ILogger<MockBeasScriptClient> log) : IBeasScriptClient
{
    public Task<BeasResult> RunScriptAsync(string requestId, string script, BeasConnection? conn, CancellationToken ct = default, IReadOnlyList<BeasSqlStatement>? pre = null)
    {
        log.LogInformation("MOCK script {RequestId}\n{Script}", requestId, script);
        // Data 回傳將送出的 script(與會先執行的 SQL),方便在 Swagger 上檢查內容
        return Task.FromResult(new BeasResult(true, "[MOCK] 未實際寫入資料庫",
            new { requestId, script, preStatements = pre?.Select(s => new { s.Sql, args = s.Args }) }));
    }
}

public class OdbcBeasScriptClient(IOptions<BeasConnection> defaults, IOptions<BeasScriptOptions> opt) : IBeasScriptClient
{
    public async Task<BeasResult> RunScriptAsync(string requestId, string script, BeasConnection? conn, CancellationToken ct = default, IReadOnlyList<BeasSqlStatement>? pre = null)
    {
        if (pre is { Count: > 0 } && !opt.Value.AllowDirectSqlWrite)
            throw new BeasDirectSqlDisabledException("直接寫入 Beas 資料表未啟用(Beas:Script:AllowDirectSqlWrite=false)");
        var c = conn ?? defaults.Value;
        var o = opt.Value;
        if (string.IsNullOrWhiteSpace(o.OdbcConnectionString))
            throw new InvalidOperationException("Beas:Script:OdbcConnectionString 未設定");
        // database 同時用作 schema 名稱(與原程式 database + ".\"BEAS_COMMON_INPUT\"" 一致)
        if (!System.Text.RegularExpressions.Regex.IsMatch(c.Database, @"^[A-Za-z0-9_]+$"))
            throw new ArgumentException("Database 名稱含不合法字元");

        var cs = o.OdbcConnectionString
            .Replace("{server}", c.Server).Replace("{database}", c.Database)
            .Replace("{user}", c.User).Replace("{password}", c.Password);

        await using var con = new OdbcConnection(cs);
        await con.OpenAsync(ct);

        // 1. 寫入(參數化,避免 script 內的單引號破壞 SQL)
        await using (var tx = await con.BeginTransactionAsync(ct))
        {
            if (pre is not null)
            {
                try { foreach (var s in pre) OdbcSql.Run(con, (OdbcTransaction)tx, s, c.Database); }
                catch (OdbcException ex) { await tx.RollbackAsync(ct); return new BeasResult(false, "前置 SQL 失敗,已還原: " + ex.Message); }
            }
            using var ins = new OdbcCommand(
                $@"INSERT INTO {c.Database}.""BEAS_COMMON_INPUT"" (COMMONTYP,TEXTPARAMETER,REQUESTID) VALUES ('script', ?, ?)",
                con, (OdbcTransaction)tx);
            ins.Parameters.AddWithValue("p1", script);
            ins.Parameters.AddWithValue("p2", requestId);
            if (ins.ExecuteNonQuery() == 0) { await tx.RollbackAsync(ct); return new BeasResult(false, "寫入 BEAS_COMMON_INPUT 失敗"); }
            await tx.CommitAsync(ct);
        }

        // 2. 輪詢結果
        for (int i = 0; i < o.MaxPolls; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(o.PollIntervalSeconds), ct);
            using var sel = new OdbcCommand(
                $@"SELECT ""ANSWERTEXT"" FROM {c.Database}.""BEAS_COMMON_OUTPUT"" WHERE ""REQUESTID""=? ORDER BY ""COUNTID"" DESC", con);
            sel.Parameters.AddWithValue("p1", requestId);
            using var dr = sel.ExecuteReader();
            while (dr.Read())
                if (!dr.IsDBNull(0) && dr.GetString(0) != "")
                    return new BeasResult(true, "完成", dr.GetString(0));
        }

        // 3. 逾時:刪除尚未被處理的 INPUT(原程式的逾時判斷永遠不成立,這裡修正)
        using (var del = new OdbcCommand($@"DELETE FROM {c.Database}.""BEAS_COMMON_INPUT"" WHERE ""REQUESTID""=?", con))
        {
            del.Parameters.AddWithValue("p1", requestId);
            del.ExecuteNonQuery();
        }
        return new BeasResult(false, "逾時:請檢查 SAP/Beas 服務是否有開啟");
    }
}
