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
}

public record BeasResult(bool Success, string Message, object? Data = null);
