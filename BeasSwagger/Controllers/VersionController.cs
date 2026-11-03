using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/version")]
public class VersionController(BeasScriptRunner runner) : ControllerBase
{
    /// <summary>建立版本</summary>
    /// <remarks>BSL <c>ItemVersion</c> POST。只有 Bsl 版;版本已存在時會被更新(upsert)。</remarks>
    [HttpPost("create")]
    public async Task<ActionResult<BeasResult>> Create(VersionCreateRequest req, CancellationToken ct) =>
        Ok(await runner.RunAsync(req, null, () => BeasBslBuilder.VersionCreate(req), ct));

    /// <summary>更新版本</summary>
    /// <remarks>BSL <c>ItemVersion</c> POST。只有 Bsl 版;只更新有填的欄位(Description、Znr→DrawingNumber),Remark 無對應欄位不送出。</remarks>
    [HttpPost("update")]
    public async Task<ActionResult<BeasResult>> Update(VersionUpdateRequest req, CancellationToken ct) =>
        Ok(await runner.RunAsync(req, null, () => BeasBslBuilder.VersionUpdate(req), ct));

}
