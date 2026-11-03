using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/unit")]
public class UnitController(IBeasCommandClient command, IBeasQueryClient query) : ControllerBase
{
    /// <summary>原 DLL 成功回 "OK"、失敗回 "NO"(影響 0 筆)。</summary>
    static BeasResult Ok(BeasResult r) => r.Success ? r with { Message = "OK" } : r;

    /// <summary>新增單位</summary>
    /// <remarks>DLL 函數:<c>CreateUom.Create</c>。直接 INSERT <c>BEAS_ME</c>(原 DLL 如此;ErfTStamp=今天、ErfUser="MES" 固定)。
    /// 需 <c>Beas:Script:AllowDirectSqlWrite=true</c>,否則 HTTP 403。單位代碼已存在時由資料庫的主鍵限制擋下。</remarks>
    [HttpPost("create")]
    public async Task<ActionResult<BeasResult>> Create(UnitCreateRequest req, CancellationToken ct) =>
        base.Ok(Ok(await command.ExecuteAsync(BeasScriptBuilder.UnitCreateStatements(req), req.Connection, ct)));

    /// <summary>更新單位</summary>
    /// <remarks>DLL 函數:<c>CreateUom.Update</c>。直接 UPDATE <c>BEAS_ME</c>(AndTStamp=今天)。找不到該單位回 "NO"。需 <c>AllowDirectSqlWrite=true</c>。</remarks>
    [HttpPost("update")]
    public async Task<ActionResult<BeasResult>> Update(UnitUpdateRequest req, CancellationToken ct) =>
        base.Ok(Ok(await command.ExecuteAsync(BeasScriptBuilder.UnitUpdateStatements(req), req.Connection, ct)));

    /// <summary>回傳結果(單位是否存在)</summary>
    /// <remarks>DLL 函數:<c>CreateUom.sqlresultback</c>。存在回該單位代碼,不存在回 "no"。這個是讀取,不需要 AllowDirectSqlWrite。</remarks>
    [HttpPost("sql-result-back")]
    public async Task<ActionResult<BeasResult>> SqlResultBack(UnitResultBackRequest req, CancellationToken ct)
    {
        var q = BeasScriptBuilder.UnitExistsQuery(req.Meid);
        var rows = await query.QueryAsync(q.Sql, q.Args, req.Connection, ct);
        if (rows.Count > 0 && rows[0].ContainsKey("MOCK_SQL"))
            return base.Ok(new BeasResult(true, "[MOCK] 未實際查詢資料庫", new { q.Sql, args = q.Args }));
        var id = rows.Select(r => r.Values.FirstOrDefault()?.ToString()).FirstOrDefault(v => !string.IsNullOrEmpty(v));
        return base.Ok(id is null ? new BeasResult(false, "no") : new BeasResult(true, id, id));
    }
}
