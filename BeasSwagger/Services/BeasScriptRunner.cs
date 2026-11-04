using BeasSwagger.Models;
using Microsoft.Extensions.Options;

namespace BeasSwagger.Services;

/// <summary>Beas 接口種類。</summary>
public enum BeasInterface
{
    /// <summary>BeasScript 內建物件(ue_api_wo / ue_api_bom / ue_reservation / ue_api_time ...)。</summary>
    Script,
    /// <summary>Beas Service Layer(script 內用 ue_bsl 呼叫 OData),經 BEAS_COMMON_INPUT 送出。</summary>
    Bsl,
    /// <summary>直接以 HTTP 呼叫 Beas Service Layer(OData4,Login → 請求 → Logout),不經資料庫;回應即結果,不需輪詢。設定見 Beas:Bsl。</summary>
    BslHttp,
}

public class BeasOptions
{
    /// <summary>預設接口;單一請求可用 Interface 欄位覆寫。</summary>
    public BeasInterface Interface { get; set; } = BeasInterface.Script;
}

public class BeasInterfaceNotSupportedException(string message) : Exception(message);

/// <summary>依接口選擇對應的 script 產生器,再交給 IBeasScriptClient 執行;BslHttp 則把同一份 BSL 內容直接送到 BSL。</summary>
public class BeasScriptRunner(IBeasScriptClient client, IBeasBslHttpClient bslHttp, IOptions<BeasOptions> opt)
{
    /// <summary>
    /// 依接口選擇 script 產生器。規則:
    /// 選 Bsl / BslHttp 但該端點沒有 Bsl 版 → 501(不會偷偷改用 Script);
    /// 選 Script 但該端點只有 Bsl 版 → 明確指定才 501,未指定(使用全域預設)則自動用 Bsl。
    /// pre(前置 SQL)只在 Script 路徑使用。
    /// </summary>
    public Task<BeasResult> RunAsync(BeasRequest req, Func<(string requestId, string script)>? script,
        Func<BslPlan>? bsl, CancellationToken ct, IReadOnlyList<BeasSqlStatement>? pre = null)
    {
        var which = req.Interface ?? opt.Value.Interface;
        if (which == BeasInterface.Script && script is null)
        {
            if (req.Interface == BeasInterface.Script || bsl is null)
                throw new BeasInterfaceNotSupportedException("此功能只有 Beas Service Layer 版本,請改用 Interface=Bsl(或不指定)");
            which = BeasInterface.Bsl;
        }
        if (which == BeasInterface.Script)
        {
            var built = script!();
            return client.RunScriptAsync(built.requestId, built.script, req.Connection, ct, pre);
        }
        if (bsl is null)
            throw new BeasInterfaceNotSupportedException("此功能尚未實作 Beas Service Layer 版本,請改用 Interface=Script");
        var plan = bsl();
        return which == BeasInterface.BslHttp
            ? bslHttp.RunAsync(plan.RequestId, plan.Calls, ct)
            : client.RunScriptAsync(plan.RequestId, plan.Script, req.Connection, ct, null);
    }
}
