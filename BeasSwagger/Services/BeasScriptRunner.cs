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
    public Task<BeasResult> RunAsync(BeasRequest req, Func<(string requestId, string script)> script,
        Func<(string requestId, string script)>? bsl, CancellationToken ct)
    {
        var which = req.Interface ?? opt.Value.Interface;
        (string id, string text) built;
        if (which == BeasInterface.Bsl)
        {
            if (bsl is null)
                throw new BeasInterfaceNotSupportedException("此功能尚未實作 Beas Service Layer 版本,請改用 Interface=Script");
            built = bsl();
        }
        else built = script();
        return client.RunScriptAsync(built.id, built.text, req.Connection, ct);
    }
}
