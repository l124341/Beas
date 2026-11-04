namespace BeasSwagger.Services;

/// <summary>
/// 一次 Beas Service Layer 呼叫。Method 為 POST 或 PUT;Resource 為 OData 資源路徑(例如 <c>IssueWo</c>、<c>WorkorderBom(1,10,20)</c>);
/// Json 為請求本文(與 script 版 <c>b.post=Entity=&lt;json&gt;</c> 送出的 JSON 相同);ErrPrefix 用來標示是哪一步失敗。
/// </summary>
public record BslCall(string Method, string Resource, string Json, string ErrPrefix);

/// <summary>
/// BSL 版 builder 的產出:同一份內容的兩種表示。
/// <see cref="Script"/> 是寫入 BEAS_COMMON_INPUT 的 BeasScript(<c>Interface=Bsl</c>);
/// <see cref="Calls"/> 是直接送到 BSL 的 HTTP 呼叫(<c>Interface=BslHttp</c>)。
/// </summary>
public record BslPlan(string RequestId, string Script, IReadOnlyList<BslCall> Calls);
