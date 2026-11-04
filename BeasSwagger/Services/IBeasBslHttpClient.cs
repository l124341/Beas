using System.Text;
using System.Text.Json;
using BeasSwagger.Models;
using Microsoft.Extensions.Options;

namespace BeasSwagger.Services;

/// <summary>直接呼叫 Beas Service Layer(OData4)的設定,對應 appsettings 的 Beas:Bsl。</summary>
public class BeasBslOptions
{
    /// <summary>BSL 服務根網址,例如 http://192.168.0.10:8080/odata4(需先在 Beas Tools > WEB Server 啟動)。</summary>
    public string BaseUrl { get; set; } = "";
    /// <summary>登入名稱:人員主檔 Login 頁籤的 Name(BSL 文件:User)。</summary>
    public string User { get; set; } = "";
    public string Password { get; set; } = "";
    /// <summary>(選填)Administration > Utilities > Beas Manage Server > Extended > BSL Service 定義的服務密碼。</summary>
    public string? ServicePwd { get; set; }
    public string? StationId { get; set; }
    public string? StationPwd { get; set; }
    public string? LocationId { get; set; }
    public string? ProgramId { get; set; }
    public int TimeoutSeconds { get; set; } = 30;
}

/// <summary>登入 BSL → 依序送出呼叫 → 登出(每個請求各自登入登出)。</summary>
public interface IBeasBslHttpClient
{
    Task<BeasResult> RunAsync(string requestId, IReadOnlyList<BslCall> calls, CancellationToken ct = default);
}

public class MockBeasBslHttpClient(ILogger<MockBeasBslHttpClient> log) : IBeasBslHttpClient
{
    public Task<BeasResult> RunAsync(string requestId, IReadOnlyList<BslCall> calls, CancellationToken ct = default)
    {
        foreach (var c in calls) log.LogInformation("MOCK BSL {Method} {Resource} ({RequestId})", c.Method, c.Resource, requestId);
        // Data 回傳將送出的呼叫(不含帳密),方便在 Swagger 上檢查內容
        return Task.FromResult(new BeasResult(true, "[MOCK] 未實際呼叫 BSL",
            new { requestId, calls = calls.Select(c => new { c.Method, c.Resource, body = ParseOrRaw(c.Json) }) }));
    }

    internal static object? ParseOrRaw(string json)
    {
        try { return JsonSerializer.Deserialize<JsonElement>(json); } catch (JsonException) { return json; }
    }
}

public class BeasBslHttpClient(HttpClient http, IOptions<BeasBslOptions> opt, ILogger<BeasBslHttpClient> log) : IBeasBslHttpClient
{
    public async Task<BeasResult> RunAsync(string requestId, IReadOnlyList<BslCall> calls, CancellationToken ct = default)
    {
        var o = opt.Value;
        if (string.IsNullOrWhiteSpace(o.BaseUrl)) throw new InvalidOperationException("Beas:Bsl:BaseUrl 未設定");
        if (string.IsNullOrWhiteSpace(o.User)) throw new InvalidOperationException("Beas:Bsl:User 未設定");
        var root = o.BaseUrl.TrimEnd('/');

        string? sid = null;
        try
        {
            // 1. 登入(取得 beas-sessionid)
            var login = new Dictionary<string, string> { ["User"] = o.User, ["Pwd"] = o.Password };
            void Opt(string k, string? v) { if (!string.IsNullOrEmpty(v)) login[k] = v; }
            Opt("ServicePwd", o.ServicePwd); Opt("StationId", o.StationId); Opt("StationPwd", o.StationPwd);
            Opt("LocationId", o.LocationId); Opt("ProgramId", o.ProgramId);
            var (loginRes, loginBody) = await SendAsync("POST", root + "/Login", JsonSerializer.Serialize(login), null, ct);
            if (!loginRes.ok) return new BeasResult(false, "BSL 登入失敗: " + loginRes.error, loginBody);
            sid = loginBody.ValueKind == JsonValueKind.Object && loginBody.TryGetProperty("beas-sessionid", out var s) && s.ValueKind == JsonValueKind.String
                ? s.GetString() : null;
            if (string.IsNullOrEmpty(sid)) return new BeasResult(false, "BSL 登入失敗: 回應沒有 beas-sessionid", loginBody);

            // 2. 依序送出;任何一步失敗就停止(與 script 版 return failure 一致)
            var results = new List<JsonElement>();
            foreach (var c in calls)
            {
                log.LogInformation("BSL {Method} {Resource} ({RequestId})", c.Method, c.Resource, requestId);
                var (r, body) = await SendAsync(c.Method, root + "/" + c.Resource, c.Json, sid, ct);
                if (!r.ok) return new BeasResult(false, $"{c.ErrPrefix}: {r.error}", body);
                results.Add(body);
            }
            // 單一呼叫直接回傳其回應;多個呼叫回傳陣列(順序與送出相同)
            return new BeasResult(true, "完成", results.Count == 1 ? results[0] : results);
        }
        catch (HttpRequestException ex) { return new BeasResult(false, "無法連線 BSL: " + ex.Message); }
        catch (TaskCanceledException) when (!ct.IsCancellationRequested) { return new BeasResult(false, "BSL 逾時"); }
        finally
        {
            if (sid is not null)
            {
                // 登出盡力而為,失敗不影響結果
                try { await SendAsync("POST", root + "/Logout", "{}", sid, CancellationToken.None); }
                catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException) { log.LogWarning("BSL Logout 失敗: {Message}", ex.Message); }
            }
        }
    }

    /// <summary>送出一個請求。成功條件:HTTP 2xx,且回應 JSON 若有 ret_code 則必須為 0(BSL 的錯誤碼:0 = ok)。</summary>
    async Task<((bool ok, string error) status, JsonElement body)> SendAsync(string method, string url, string json, string? sid, CancellationToken ct)
    {
        using var req = new HttpRequestMessage(new HttpMethod(method), url) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
        if (sid is not null) req.Headers.TryAddWithoutValidation("Cookie", "beas-sessionid=" + sid);
        using var res = await http.SendAsync(req, ct);
        var text = await res.Content.ReadAsStringAsync(ct);

        JsonElement body;
        try { body = JsonSerializer.Deserialize<JsonElement>(text); }
        catch (JsonException) { body = JsonSerializer.SerializeToElement(text); } // 非 JSON:原文當字串回傳

        if (body.ValueKind == JsonValueKind.Object && body.TryGetProperty("ret_code", out var rc) && RetCode(rc) is int code && code != 0)
        {
            var msg = body.TryGetProperty("ret_text", out var rt) && rt.ValueKind == JsonValueKind.String ? rt.GetString() : null;
            return ((false, $"({code}) {msg ?? "BSL 回傳錯誤"}"), body);
        }
        if (!res.IsSuccessStatusCode) return ((false, $"HTTP {(int)res.StatusCode} {res.ReasonPhrase}"), body);
        return ((true, ""), body);
    }

    static int? RetCode(JsonElement e) => e.ValueKind switch
    {
        JsonValueKind.Number when e.TryGetInt32(out var n) => n,
        JsonValueKind.String when int.TryParse(e.GetString(), out var n) => n,
        _ => null,
    };
}
