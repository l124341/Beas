using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/unit")]
public class UnitController(IBeasGateway gw) : ControllerBase
{
    /// <summary>新增單位</summary>
    /// <remarks>DLL 函數:<c>Create</c></remarks>
    [HttpPost("create")]
    public async Task<ActionResult<BeasResult>> Create(UnitCreateRequest req) =>
        Ok(await gw.InvokeAsync("Unit.Create", req));

    /// <summary>更新單位</summary>
    /// <remarks>DLL 函數:<c>Update</c></remarks>
    [HttpPost("update")]
    public async Task<ActionResult<BeasResult>> Update(UnitUpdateRequest req) =>
        Ok(await gw.InvokeAsync("Unit.Update", req));

    /// <summary>回傳結果</summary>
    /// <remarks>DLL 函數:<c>sqlresultback</c></remarks>
    [HttpPost("sql-result-back")]
    public async Task<ActionResult<BeasResult>> SqlResultBack(SqlResultBackRequest req) =>
        Ok(await gw.InvokeAsync("Unit.sqlresultback", req));

}
