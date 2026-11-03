using BeasSwagger.Models;
using BeasSwagger.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeasSwagger.Controllers;

[ApiController]
[Route("api/workorder")]
public class WorkOrderController(IBeasGateway gw) : ControllerBase
{
    /// <summary>單一匯入</summary>
    /// <remarks>DLL 函數:<c>import_work_order_single</c></remarks>
    [HttpPost("import-single")]
    public async Task<ActionResult<BeasResult>> ImportSingle(WorkOrderSingleRequest req) =>
        Ok(await gw.InvokeAsync("WorkOrder.import_work_order_single", req));

    /// <summary>多筆匯入</summary>
    /// <remarks>DLL 函數:<c>import_work_order_more</c></remarks>
    [HttpPost("import-more")]
    public async Task<ActionResult<BeasResult>> ImportMore(WorkOrderMoreRequest req) =>
        Ok(await gw.InvokeAsync("WorkOrder.import_work_order_more", req));

    /// <summary>工單傳入委單機型 工時</summary>
    /// <remarks>DLL 函數:<c>import_work_order_withstalkWithmachinewithwortime</c></remarks>
    [HttpPost("import-with-machine")]
    public async Task<ActionResult<BeasResult>> ImportWithMachine(WorkOrderWithMachineRequest req) =>
        Ok(await gw.InvokeAsync("WorkOrder.import_work_order_withstalkWithmachinewithwortime", req));

    /// <summary>修改工單BOM</summary>
    /// <remarks>DLL 函數:<c>changeworkorederbom</c></remarks>
    [HttpPost("change-bom")]
    public async Task<ActionResult<BeasResult>> ChangeBom(ChangeWorkOrderBomRequest req) =>
        Ok(await gw.InvokeAsync("WorkOrder.changeworkorederbom", req));

    /// <summary>回傳刪工單列訊息成功與否</summary>
    /// <remarks>DLL 函數:<c>delresultback</c></remarks>
    [HttpPost("del-result-back")]
    public async Task<ActionResult<BeasResult>> DelResultBack(DelResultBackRequest req) =>
        Ok(await gw.InvokeAsync("WorkOrder.delresultback", req));

}
