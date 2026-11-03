using BeasSwagger.Services;

namespace BeasSwagger.Models;

/// <summary>SAP B1 資料庫連線資訊(對應 DLL 每個函數的 server/database/user/password)</summary>
public class BeasConnection
{
    public string Server { get; set; } = "";
    public string Database { get; set; } = "";
    public string User { get; set; } = "";
    public string Password { get; set; } = "";
}

/// <summary>所有請求的基底:Connection 留空則用 appsettings 預設值</summary>
public abstract class BeasRequest
{
    /// <summary>(選填)覆寫連線資訊</summary>
    public BeasConnection? Connection { get; set; }

    /// <summary>(選填)指定 Beas 接口:Script 或 Bsl;留空則用 appsettings 的 Beas:Interface。目前所有功能只有 Script 版。</summary>
    public BeasInterface? Interface { get; set; }
}

public record BeasResult(bool Success, string Message, object? Data = null);
