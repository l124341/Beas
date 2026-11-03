using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/production")]
public class ProductionController(IBeasGateway gw, IBeasScriptClient script) : ControllerBase
{
    /// <summary>發料(工單領料)</summary>
    /// <remarks>寫入 BEAS_COMMON_INPUT 並等待 BEAS_COMMON_OUTPUT 回傳 DocEntry;最久約 MaxPolls × PollIntervalSeconds 秒。
    /// Mock 模式只回傳將送出的 script,不寫資料庫。</remarks>
    [HttpPost("issue")]
    public async Task<ActionResult<BeasResult>> Issue(IssueRequest req, CancellationToken ct)
    {
        var (id, text) = BeasScriptBuilder.Issue(req.OigeList);
        return Ok(await script.RunScriptAsync(id, text, req.Connection, ct));
    }

    /// <summary>完工回報</summary>
    /// <remarks>DLL 函數:<c>finished</c></remarks>
    [HttpPost("finished")]
    public async Task<ActionResult<BeasResult>> Finished(FinishedRequest req) =>
        Ok(await gw.InvokeAsync("Production.finished", req));

    /// <summary>入庫 or 半成品退回</summary>
    /// <remarks>DLL 函數:<c>returngood</c></remarks>
    [HttpPost("return-good")]
    public async Task<ActionResult<BeasResult>> ReturnGood(ReturnGoodRequest req) =>
        Ok(await gw.InvokeAsync("Production.returngood", req));

    /// <summary>成品入庫</summary>
    /// <remarks>DLL 函數:<c>finishedrecipt</c></remarks>
    [HttpPost("finished-receipt")]
    public async Task<ActionResult<BeasResult>> FinishedReceipt(FinishedReceiptRequest req) =>
        Ok(await gw.InvokeAsync("Production.finishedrecipt", req));

    /// <summary>回傳結果</summary>
    /// <remarks>DLL 函數:<c>sqlresultback</c></remarks>
    [HttpPost("sql-result-back")]
    public async Task<ActionResult<BeasResult>> SqlResultBack(SqlResultBackRequest req) =>
        Ok(await gw.InvokeAsync("Production.sqlresultback", req));

}
