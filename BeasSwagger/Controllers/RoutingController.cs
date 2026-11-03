using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/routing")]
public class RoutingController(BeasScriptRunner runner, IBeasCommandClient command, BeasResultReader reader) : ControllerBase
{
    /// <summary>增加工藝</summary>
    /// <remarks>DLL 函數:<c>add</c></remarks>
    [HttpPost("add")]
    public async Task<ActionResult<BeasResult>> Add(RoutingRequest req, CancellationToken ct)
    {
        return Ok(await runner.RunAsync(req, () => BeasScriptBuilder.RoutingAdd(req),
            () => BeasBslBuilder.RoutingAdd(req), ct));
    }

    /// <summary>更新工藝(整條取代)</summary>
    /// <remarks>DLL 函數:<c>change</c>。與原 DLL 相同:先刪除該料號的整條途程(BEAS_APL_WKZ、BEAS_APL),再建立只有這一個工序的新途程,
    /// 刪除與寫入 INPUT 在同一個交易。**會移除該料號其他所有工序**。需 <c>Beas:Script:AllowDirectSqlWrite=true</c>;只有 Script 版。</remarks>
    [HttpPost("change")]
    public async Task<ActionResult<BeasResult>> Change(RoutingRequest req, CancellationToken ct) =>
        Ok(await runner.RunAsync(req, () => BeasScriptBuilder.RoutingChange(req), null, ct,
            BeasScriptBuilder.RoutingDeleteStatements(req.Item)));

    /// <summary>刪除工藝(依 ItemCode,整條途程)</summary>
    /// <remarks>DLL 函數:<c>delete</c>。直接對 BEAS_APL_WKZ、BEAS_APL 下 DELETE(同一交易)。需 <c>Beas:Script:AllowDirectSqlWrite=true</c>。</remarks>
    [HttpPost("delete")]
    public async Task<ActionResult<BeasResult>> Delete(ItemRequest req, CancellationToken ct) =>
        Ok(await command.ExecuteAsync(BeasScriptBuilder.RoutingDeleteStatements(req.ItemCode), req.Connection, ct));

    /// <summary>回傳結果(途程)</summary>
    /// <remarks>DLL 函數:<c>sqlresultback</c>。REQUESTID = "Rot:" + Mes + CreateTime。</remarks>
    [HttpPost("sql-result-back")]
    public async Task<ActionResult<BeasResult>> SqlResultBack(SqlResultBackRequest req, CancellationToken ct) =>
        Ok(await reader.ReadAsync(string.IsNullOrWhiteSpace(req.RequestId) ? "Rot:" + req.Mes + req.CreateTime : req.RequestId, req.Connection, ct));

}
