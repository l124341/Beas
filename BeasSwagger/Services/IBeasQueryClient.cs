using System.Data.Odbc;
using System.Text.RegularExpressions;
using BeasSwagger.Models;
using Microsoft.Extensions.Options;

namespace BeasSwagger.Services;

/// <summary>唯讀查詢 Beas 資料表(只允許單一 SELECT,值一律參數化)。與 BEAS_COMMON_INPUT 流程無關。</summary>
public interface IBeasQueryClient
{
    Task<List<Dictionary<string, object?>>> QueryAsync(string sql, object?[] args, BeasConnection? conn, CancellationToken ct = default);
}

public class MockBeasQueryClient(ILogger<MockBeasQueryClient> log) : IBeasQueryClient
{
    public Task<List<Dictionary<string, object?>>> QueryAsync(string sql, object?[] args, BeasConnection? conn, CancellationToken ct = default)
    {
        log.LogInformation("MOCK query {Sql} [{Args}]", sql, string.Join(",", args));
        // Mock:回一列說明資料,方便在 Swagger 檢查將執行的 SQL
        return Task.FromResult(new List<Dictionary<string, object?>>
        {
            new() { ["MOCK_SQL"] = sql, ["MOCK_ARGS"] = string.Join(",", args) }
        });
    }
}

public class OdbcBeasQueryClient(IOptions<BeasConnection> defaults, IOptions<BeasScriptOptions> opt) : IBeasQueryClient
{
    public async Task<List<Dictionary<string, object?>>> QueryAsync(string sql, object?[] args, BeasConnection? conn, CancellationToken ct = default)
    {
        var c = conn ?? defaults.Value;
        if (!sql.TrimStart().StartsWith("select", StringComparison.OrdinalIgnoreCase) || sql.Contains(';'))
            throw new ArgumentException("只允許單一 SELECT");
        if (!Regex.IsMatch(c.Database, @"^[A-Za-z0-9_]+$")) throw new ArgumentException("Database 名稱含不合法字元");
        var template = opt.Value.OdbcConnectionString;
        if (string.IsNullOrWhiteSpace(template)) throw new InvalidOperationException("Beas:Script:OdbcConnectionString 未設定");
        var cs = template.Replace("{server}", c.Server).Replace("{database}", c.Database)
            .Replace("{user}", c.User).Replace("{password}", c.Password);

        await using var con = new OdbcConnection(cs);
        await con.OpenAsync(ct);
        using var cmd = new OdbcCommand(sql.Replace("{db}", c.Database), con);
        for (int i = 0; i < args.Length; i++) cmd.Parameters.AddWithValue("p" + i, args[i] ?? DBNull.Value);
        using var dr = cmd.ExecuteReader();
        var rows = new List<Dictionary<string, object?>>();
        while (await dr.ReadAsync(ct))
        {
            var row = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < dr.FieldCount; i++) row[dr.GetName(i)] = dr.IsDBNull(i) ? null : dr.GetValue(i);
            rows.Add(row);
        }
        return rows;
    }
}
