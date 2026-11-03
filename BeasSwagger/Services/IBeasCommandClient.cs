using System.Data.Odbc;
using System.Text.RegularExpressions;
using BeasSwagger.Models;
using Microsoft.Extensions.Options;

namespace BeasSwagger.Services;

/// <summary>由程式碼固定產生的寫入 SQL(不接受使用者提供的 SQL)。{db} 會被替換成已驗證的資料庫名稱,值一律用 ? 參數。</summary>
// RequireRows:true = 影響 0 筆視為失敗並還原整個交易(對應原 DLL 的 if (result == 0) → "NO")。僅 IBeasCommandClient 使用。
public record BeasSqlStatement(string Sql, object?[] Args, bool RequireRows = false);

/// <summary>需要明確開啟 Beas:Script:AllowDirectSqlWrite 才允許直接寫入 Beas 資料表。</summary>
public class BeasDirectSqlDisabledException(string message) : Exception(message);

/// <summary>在同一個交易內執行直接 SQL 寫入(對應原 DLL routing.delete 對 BEAS_APL / BEAS_APL_WKZ 的 DELETE)。</summary>
public interface IBeasCommandClient
{
    Task<BeasResult> ExecuteAsync(IReadOnlyList<BeasSqlStatement> statements, BeasConnection? conn, CancellationToken ct = default);

    /// <summary>在碰資料庫之前先檢查是否允許直接寫入(未啟用時丟 BeasDirectSqlDisabledException)。Mock 模式不檢查,以便預覽。</summary>
    void EnsureWriteAllowed();
}

public class MockBeasCommandClient(ILogger<MockBeasCommandClient> log) : IBeasCommandClient
{
    public void EnsureWriteAllowed() { }

    public Task<BeasResult> ExecuteAsync(IReadOnlyList<BeasSqlStatement> statements, BeasConnection? conn, CancellationToken ct = default)
    {
        foreach (var s in statements) log.LogInformation("MOCK sql {Sql} [{Args}]", s.Sql, string.Join(",", s.Args));
        return Task.FromResult(new BeasResult(true, "[MOCK] 未實際執行 SQL",
            new { statements = statements.Select(s => new { s.Sql, args = s.Args }) }));
    }
}

public class OdbcBeasCommandClient(IOptions<BeasConnection> defaults, IOptions<BeasScriptOptions> opt) : IBeasCommandClient
{
    public void EnsureWriteAllowed()
    {
        if (!opt.Value.AllowDirectSqlWrite)
            throw new BeasDirectSqlDisabledException("直接寫入 Beas 資料表未啟用(Beas:Script:AllowDirectSqlWrite=false)");
    }

    public async Task<BeasResult> ExecuteAsync(IReadOnlyList<BeasSqlStatement> statements, BeasConnection? conn, CancellationToken ct = default)
    {
        var o = opt.Value;
        if (!o.AllowDirectSqlWrite)
            throw new BeasDirectSqlDisabledException("直接寫入 Beas 資料表未啟用(Beas:Script:AllowDirectSqlWrite=false)");
        var c = conn ?? defaults.Value;
        if (!Regex.IsMatch(c.Database, @"^[A-Za-z0-9_]+$")) throw new ArgumentException("Database 名稱含不合法字元");
        if (string.IsNullOrWhiteSpace(o.OdbcConnectionString)) throw new InvalidOperationException("Beas:Script:OdbcConnectionString 未設定");
        var cs = o.OdbcConnectionString.Replace("{server}", c.Server).Replace("{database}", c.Database)
            .Replace("{user}", c.User).Replace("{password}", c.Password);

        await using var con = new OdbcConnection(cs);
        await con.OpenAsync(ct);
        await using var tx = await con.BeginTransactionAsync(ct);
        try
        {
            foreach (var s in statements)
            {
                if (OdbcSql.Run(con, (OdbcTransaction)tx, s, c.Database) == 0 && s.RequireRows)
                {
                    await tx.RollbackAsync(ct);
                    return new BeasResult(false, "NO");
                }
            }
            await tx.CommitAsync(ct);
            return new BeasResult(true, "success");
        }
        catch (OdbcException ex)
        {
            await tx.RollbackAsync(ct);
            return new BeasResult(false, "執行失敗,已還原: " + ex.Message);
        }
    }
}

static class OdbcSql
{
    public static int Run(OdbcConnection con, OdbcTransaction tx, BeasSqlStatement s, string database)
    {
        using var cmd = new OdbcCommand(s.Sql.Replace("{db}", database), con, tx);
        for (int i = 0; i < s.Args.Length; i++) cmd.Parameters.AddWithValue("p" + i, s.Args[i] ?? DBNull.Value);
        return cmd.ExecuteNonQuery();
    }
}
