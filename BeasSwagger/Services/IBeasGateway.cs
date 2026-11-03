using BeasSwagger.Models;

namespace BeasSwagger.Services;

public interface IBeasGateway
{
    /// <param name="function">對應 Excel「函數名稱」,例如 Bom.CreateM</param>
    /// <param name="args">請求內容</param>
    Task<BeasResult> InvokeAsync(string function, BeasRequest args);
}

/// <summary>假實作:僅回傳收到的參數,用來先測 Swagger 介面。</summary>
public class MockBeasGateway(ILogger<MockBeasGateway> log) : IBeasGateway
{
    public Task<BeasResult> InvokeAsync(string function, BeasRequest args)
    {
        log.LogInformation("MOCK {Function}", function);
        return Task.FromResult(new BeasResult(true, $"[MOCK] {function} OK", args));
    }
}

