using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/bom")]
public class BomController(IBeasGateway gw) : ControllerBase
{
    /// <summary>建立BOM多明細</summary>
    /// <remarks>DLL 函數:<c>CreateM</c></remarks>
    [HttpPost("create")]
    public async Task<ActionResult<BeasResult>> Create(BomCreateRequest req) =>
        Ok(await gw.InvokeAsync("Bom.CreateM", req));

    /// <summary>尋找其他版本的BOM</summary>
    /// <remarks>DLL 函數:<c>Searchotherversionbom</c></remarks>
    [HttpPost("search-other-version")]
    public async Task<ActionResult<BeasResult>> SearchOtherVersion(BomSearchOtherVersionRequest req) =>
        Ok(await gw.InvokeAsync("Bom.Searchotherversionbom", req));

    /// <summary>新版本BOM插入</summary>
    /// <remarks>DLL 函數:<c>CreateMultVer</c></remarks>
    [HttpPost("create-version")]
    public async Task<ActionResult<BeasResult>> CreateVersion(BomVersionRequest req) =>
        Ok(await gw.InvokeAsync("Bom.CreateMultVer", req));

    /// <summary>修改/更新BOM</summary>
    /// <remarks>DLL 函數:<c>ChangeBom</c></remarks>
    [HttpPost("change")]
    public async Task<ActionResult<BeasResult>> Change(BomVersionRequest req) =>
        Ok(await gw.InvokeAsync("Bom.ChangeBom", req));

    /// <summary>查看BOM</summary>
    /// <remarks>DLL 函數:<c>checkbom</c></remarks>
    [HttpPost("check")]
    public async Task<ActionResult<BeasResult>> Check(ItemRequest req) =>
        Ok(await gw.InvokeAsync("Bom.checkbom", req));

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
    public async Task<ActionResult<BeasResult>> MaxLevel(ItemRequest req) =>
        Ok(await gw.InvokeAsync("Bom.selectmaxpostext", req));

}
