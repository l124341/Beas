using BeasSwagger.Models;

namespace BeasSwagger.Services;

/// <summary>
/// 對應原 DLL 的 sqlresultback / delresultback:只讀一次 BEAS_COMMON_OUTPUT,
/// 依 REQUESTID 取 COUNTID 最大且 ANSWERTEXT 非 NULL 的那筆;查不到回「尚無結果」,由呼叫端自行重試(與原 DLL 相同,不等待)。
/// </summary>
public class BeasResultReader(IBeasQueryClient query)
{
    public async Task<BeasResult> ReadAsync(string requestId, BeasConnection? conn, CancellationToken ct)
    {
        var id = BeasScriptBuilder.V(requestId, "RequestId");
        var rows = await query.QueryAsync(
            @"select ""ANSWERTEXT"" from {db}.""BEAS_COMMON_OUTPUT"" where ""REQUESTID""=? order by ""COUNTID"" desc",
            new object?[] { id }, conn, ct);
        if (rows.Count > 0 && rows[0].ContainsKey("MOCK_SQL"))
            return new BeasResult(true, "[MOCK] 未實際查詢資料庫", new { requestId = id, rows });
        var answer = rows.Select(r => r.GetValueOrDefault("ANSWERTEXT")?.ToString()).FirstOrDefault(a => a != null);
        return answer is null
            ? new BeasResult(false, "尚無結果", new { requestId = id })
            : new BeasResult(true, "完成", answer);
    }
}
