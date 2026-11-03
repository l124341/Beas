using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/workorder")]
public class WorkOrderController(IBeasGateway gw, BeasScriptRunner runner) : ControllerBase
{
    /// <summary>單一匯入</summary>
    /// <remarks>DLL 函數:<c>import_work_order_single</c></remarks>
    [HttpPost("import-single")]
    public async Task<ActionResult<BeasResult>> ImportSingle(WorkOrderSingleRequest req, CancellationToken ct) =>
        Ok(await runner.RunAsync(req, () => BeasScriptBuilder.WorkOrderImport(
            new[] { (req.ItemCode, (decimal)req.Qty) }, ""),
            () => BeasBslBuilder.WorkOrderImport(
            new[] { (req.ItemCode, (decimal)req.Qty) }, ""), ct));

    /// <summary>多筆匯入</summary>
    /// <remarks>DLL 函數:<c>import_work_order_more</c></remarks>
    [HttpPost("import-more")]
    public async Task<ActionResult<BeasResult>> ImportMore(WorkOrderMoreRequest req, CancellationToken ct) =>
        Ok(await runner.RunAsync(req, () => BeasScriptBuilder.WorkOrderImport(
            req.InputItems.Select(i => (i.ItemCode, (decimal)i.Qty)).ToList(), req.Mes),
            () => BeasBslBuilder.WorkOrderImport(
            req.InputItems.Select(i => (i.ItemCode, (decimal)i.Qty)).ToList(), req.Mes), ct));

    /// <summary>工單傳入委單機型 工時</summary>
    /// <remarks>DLL 函數:<c>import_work_order_withstalkWithmachinewithwortime</c></remarks>
    [HttpPost("import-with-machine")]
    public async Task<ActionResult<BeasResult>> ImportWithMachine(WorkOrderWithMachineRequest req) =>
        Ok(await gw.InvokeAsync("WorkOrder.import_work_order_withstalkWithmachinewithwortime", req));

    /// <summary>修改工單BOM(取代 BOM 某行的料號)</summary>
    /// <remarks>DLL 函數:<c>changeworkorederbom</c>;Script 版:<c>ue_api_wo bom_replaceitem</c>;Bsl 版:<c>PUT WorkorderBom</c></remarks>
    [HttpPost("change-bom")]
    public async Task<ActionResult<BeasResult>> ChangeBom(ChangeWorkOrderBomRequest req, CancellationToken ct) =>
        Ok(await runner.RunAsync(req,
            () => BeasScriptBuilder.ChangeWorkOrderBom(req.InputItems, req.Mes, req.CreateTime),
            () => BeasBslBuilder.ChangeWorkOrderBom(req.InputItems, req.Mes, req.CreateTime), ct));

    /// <summary>回傳刪工單列訊息成功與否</summary>
    /// <remarks>DLL 函數:<c>delresultback</c></remarks>
    [HttpPost("del-result-back")]
    public async Task<ActionResult<BeasResult>> DelResultBack(DelResultBackRequest req) =>
        Ok(await gw.InvokeAsync("WorkOrder.delresultback", req));

}
