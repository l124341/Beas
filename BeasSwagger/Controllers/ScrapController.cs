using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/scrap")]
public class ScrapController(IBeasGateway gw) : ControllerBase
{
    /// <summary>建立報廢明細表</summary>
    /// <remarks>DLL 函數:<c>Create</c></remarks>
    [HttpPost("create")]
    public async Task<ActionResult<BeasResult>> Create(ScrapCreateRequest req) =>
        Ok(await gw.InvokeAsync("Scrap.Create", req));

}
