using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/version")]
public class VersionController(IBeasGateway gw) : ControllerBase
{
    /// <summary>建立版本</summary>
    /// <remarks>DLL 函數:<c>Create</c></remarks>
    [HttpPost("create")]
    public async Task<ActionResult<BeasResult>> Create(VersionCreateRequest req) =>
        Ok(await gw.InvokeAsync("Version.Create", req));

    /// <summary>更新版本</summary>
    /// <remarks>DLL 函數:<c>Update</c></remarks>
    [HttpPost("update")]
    public async Task<ActionResult<BeasResult>> Update(VersionUpdateRequest req) =>
        Ok(await gw.InvokeAsync("Version.Update", req));

}
