using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/scrapdetail")]
public class ScrapDetailController(IBeasGateway gw) : ControllerBase
{
    /// <summary>建立廢品明細</summary>
    /// <remarks>DLL 函數:<c>Create</c></remarks>
    [HttpPost("create")]
    public async Task<ActionResult<BeasResult>> Create(ScrapDetailCreateRequest req) =>
        Ok(await gw.InvokeAsync("ScrapDetail.Create", req));

    /// <summary>更新廢品明細</summary>
    /// <remarks>DLL 函數:<c>update</c></remarks>
    [HttpPost("update")]
    public async Task<ActionResult<BeasResult>> Update(ScrapDetailUpdateRequest req) =>
        Ok(await gw.InvokeAsync("ScrapDetail.update", req));

    /// <summary>刪除廢品明細</summary>
    /// <remarks>DLL 函數:<c>delete</c></remarks>
    [HttpPost("delete")]
    public async Task<ActionResult<BeasResult>> Delete(ScrapDetailDeleteRequest req) =>
        Ok(await gw.InvokeAsync("ScrapDetail.delete", req));

}
