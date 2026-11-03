using BeasSwagger.Models;
using Microsoft.Extensions.Options;

namespace BeasSwagger.Services;

/// <summary>Beas 接口種類。</summary>
public enum BeasInterface
{
    /// <summary>BeasScript 內建物件(ue_api_wo / ue_api_bom / ue_reservation / ue_api_time ...)。</summary>
    Script,
    /// <summary>Beas Service Layer(script 內用 ue_bsl 呼叫 OData)。</summary>
    Bsl,
}

public class BeasOptions
{
    /// <summary>預設接口;單一請求可用 Interface 欄位覆寫。</summary>
    public BeasInterface Interface { get; set; } = BeasInterface.Script;
}

public class BeasInterfaceNotSupportedException(string message) : Exception(message);

/// <summary>依接口選擇對應的 script 產生器,再交給 IBeasScriptClient 執行。</summary>
public class BeasScriptRunner(IBeasScriptClient client, IOptions<BeasOptions> opt)
{
    /// <summary>
    /// 依接口選擇 script 產生器。規則:
    /// 選 Bsl 但該端點沒有 Bsl 版 → 501(不會偷偷改用 Script);
    /// 選 Script 但該端點只有 Bsl 版 → 明確指定才 501,未指定(使用全域預設)則自動用 Bsl。
    /// pre(前置 SQL)只在 Script 路徑使用。
    /// </summary>
    public Task<BeasResult> RunAsync(BeasRequest req, Func<(string requestId, string script)>? script,
        Func<(string requestId, string script)>? bsl, CancellationToken ct, IReadOnlyList<BeasSqlStatement>? pre = null)
    {
        var which = req.Interface ?? opt.Value.Interface;
        (string id, string text) built;
        bool viaScript = false;
        if (which == BeasInterface.Bsl)
        {
            if (bsl is null)
                throw new BeasInterfaceNotSupportedException("此功能尚未實作 Beas Service Layer 版本,請改用 Interface=Script");
            built = bsl();
        }
        else if (script is null)
        {
            if (req.Interface == BeasInterface.Script || bsl is null)
                throw new BeasInterfaceNotSupportedException("此功能只有 Beas Service Layer 版本,請改用 Interface=Bsl(或不指定)");
            built = bsl();
        }
        else { built = script(); viaScript = true; }
        return client.RunScriptAsync(built.id, built.text, req.Connection, ct, viaScript ? pre : null);
    }
}
