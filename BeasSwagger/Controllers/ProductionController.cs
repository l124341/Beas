using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/production")]
public class ProductionController(BeasScriptRunner runner, BeasResultReader reader) : ControllerBase
{
    /// <summary>發料(工單領料)</summary>
    /// <remarks>寫入 BEAS_COMMON_INPUT 並等待 BEAS_COMMON_OUTPUT 回傳 DocEntry;最久約 MaxPolls × PollIntervalSeconds 秒。
    /// Mock 模式只回傳將送出的 script,不寫資料庫。</remarks>
    [HttpPost("issue")]
    public async Task<ActionResult<BeasResult>> Issue(IssueRequest req, CancellationToken ct)
    {
        return Ok(await runner.RunAsync(req, () => BeasScriptBuilder.Issue(req.OigeList),
            () => BeasBslBuilder.Issue(req.OigeList), ct));
    }

    /// <summary>完工回報</summary>
    /// <remarks>DLL 函數:<c>finished</c></remarks>
    [HttpPost("finished")]
    public async Task<ActionResult<BeasResult>> Finished(FinishedRequest req, CancellationToken ct)
    {
        return Ok(await runner.RunAsync(req, () => BeasScriptBuilder.Finished(req),
            () => BeasBslBuilder.Finished(req), ct));
    }

    /// <summary>入庫 or 半成品退回</summary>
    /// <remarks>DLL 函數:<c>returngood</c></remarks>
    [HttpPost("return-good")]
    public async Task<ActionResult<BeasResult>> ReturnGood(ReturnGoodRequest req, CancellationToken ct)
    {
        return Ok(await runner.RunAsync(req, () => BeasScriptBuilder.ReturnGood(req),
            () => BeasBslBuilder.ReturnGood(req), ct));
    }

    /// <summary>成品入庫</summary>
    /// <remarks>DLL 函數:<c>finishedrecipt</c></remarks>
    [HttpPost("finished-receipt")]
    public async Task<ActionResult<BeasResult>> FinishedReceipt(FinishedReceiptRequest req, CancellationToken ct)
    {
        return Ok(await runner.RunAsync(req, () => BeasScriptBuilder.FinishedReceipt(req),
            () => BeasBslBuilder.FinishedReceipt(req), ct));
    }

    /// <summary>回傳結果</summary>
    /// <remarks>DLL 函數:<c>sqlresultback</c></remarks>
    [HttpPost("sql-result-back")]
    public async Task<ActionResult<BeasResult>> SqlResultBack(SqlResultBackRequest req, CancellationToken ct) =>
        Ok(await reader.ReadAsync(string.IsNullOrWhiteSpace(req.RequestId) ? req.Mes + req.CreateTime : req.RequestId, req.Connection, ct));

}
