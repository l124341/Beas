using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/bom")]
public class BomController(IBeasGateway gw, BeasScriptRunner runner, IBeasQueryClient query) : ControllerBase
{
    /// <summary>建立BOM多明細</summary>
    /// <remarks>DLL 函數:<c>CreateM</c></remarks>
    [HttpPost("create")]
    public async Task<ActionResult<BeasResult>> Create(BomCreateRequest req, CancellationToken ct)
    {
        return Ok(await runner.RunAsync(req, () => BeasScriptBuilder.BomCreate(req),
            () => BeasBslBuilder.BomCreate(req), ct));
    }

    /// <summary>尋找其他版本的BOM</summary>
    /// <remarks>DLL 函數:<c>Searchotherversionbom</c></remarks>
    [HttpPost("search-other-version")]
    public async Task<ActionResult<BeasResult>> SearchOtherVersion(BomSearchOtherVersionRequest req) =>
        Ok(await gw.InvokeAsync("Bom.Searchotherversionbom", req));

    /// <summary>新版本BOM插入</summary>
    /// <remarks>DLL 函數:<c>CreateMultVer</c></remarks>
    [HttpPost("create-version")]
    public async Task<ActionResult<BeasResult>> CreateVersion(BomVersionRequest req, CancellationToken ct)
    {
        return Ok(await runner.RunAsync(req, () => BeasScriptBuilder.BomCreateVersion(req), null, ct));
    }

    /// <summary>修改/更新BOM</summary>
    /// <remarks>DLL 函數:<c>ChangeBom</c></remarks>
    [HttpPost("change")]
    public async Task<ActionResult<BeasResult>> Change(BomVersionRequest req, CancellationToken ct)
    {
        return Ok(await runner.RunAsync(req, () => BeasScriptBuilder.BomChange(req),
            () => BeasBslBuilder.BomChange(req), ct));
    }

    /// <summary>查看BOM</summary>
    /// <remarks>DLL 函數:<c>checkbom</c></remarks>
    [HttpPost("check")]
    public async Task<ActionResult<BeasResult>> Check(ItemRequest req, CancellationToken ct)
    {
        // BEAS_STL:ItemCode=BOM 編號;art1_id=子件;menge_verbrauch=用量(欄位名稱依 api_bom 說明)
        var rows = await query.QueryAsync(
            @"select ""pos_id"", ""pos_text"", ""art1_id"", ""menge_verbrauch"" from {db}.""BEAS_STL"" where ""ItemCode""=? order by ""sortid""",
            new object?[] { req.ItemCode }, req.Connection, ct);
        return Ok(new BeasResult(true, $"{rows.Count} 筆", rows));
    }

    /// <summary>查看BOM版本</summary>
    /// <remarks>DLL 函數:<c>checkbomversion</c></remarks>
    [HttpPost("check-version")]
    public async Task<ActionResult<BeasResult>> CheckVersion(ItemVersionRequest req) =>
        Ok(await gw.InvokeAsync("Bom.checkbomversion", req));

    /// <summary>刪除BOM</summary>
    /// <remarks>DLL 函數:<c>DeleteBom</c></remarks>
    [HttpPost("delete")]
    public async Task<ActionResult<BeasResult>> Delete(BomDeleteRequest req) =>
        Ok(await gw.InvokeAsync("Bom.DeleteBom", req));

    /// <summary>回傳成功與否</summary>
    /// <remarks>DLL 函數:<c>sqlresultback</c></remarks>
    [HttpPost("sql-result-back")]
    public async Task<ActionResult<BeasResult>> SqlResultBack(SqlResultBackRequest req) =>
        Ok(await gw.InvokeAsync("Bom.sqlresultback", req));

    /// <summary>取得itemcode中的最大階層</summary>
    /// <remarks>DLL 函數:<c>selectmaxpostext</c></remarks>
    [HttpPost("max-level")]
    public async Task<ActionResult<BeasResult>> MaxLevel(ItemRequest req, CancellationToken ct)
    {
        // pos_text 是字串,在 C# 端取數值最大值(避免 HANA/MSSQL 轉型語法差異)
        var rows = await query.QueryAsync(
            @"select ""pos_text"" from {db}.""BEAS_STL"" where ""ItemCode""=?",
            new object?[] { req.ItemCode }, req.Connection, ct);
        var max = rows.Select(r => r.GetValueOrDefault("pos_text")?.ToString())
                      .Where(t => decimal.TryParse(t, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _))
                      .Select(t => decimal.Parse(t!, System.Globalization.CultureInfo.InvariantCulture))
                      .DefaultIfEmpty(0).Max();
        return Ok(new BeasResult(true, "完成", max));
    }

}
