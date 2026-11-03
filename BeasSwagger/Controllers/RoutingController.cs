using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/routing")]
public class RoutingController(IBeasGateway gw) : ControllerBase
{
    /// <summary>增加工藝</summary>
    /// <remarks>DLL 函數:<c>add</c></remarks>
    [HttpPost("add")]
    public async Task<ActionResult<BeasResult>> Add(RoutingRequest req) =>
        Ok(await gw.InvokeAsync("Routing.add", req));

    /// <summary>更新工藝</summary>
    /// <remarks>DLL 函數:<c>change</c></remarks>
    [HttpPost("change")]
    public async Task<ActionResult<BeasResult>> Change(RoutingRequest req) =>
        Ok(await gw.InvokeAsync("Routing.change", req));

    /// <summary>刪除工藝(依 ItemCode)</summary>
    /// <remarks>DLL 函數:<c>delete</c></remarks>
    [HttpPost("delete")]
    public async Task<ActionResult<BeasResult>> Delete(ItemRequest req) =>
        Ok(await gw.InvokeAsync("Routing.delete", req));

}
