using System.Reflection;
using BeasSwagger.Models;
using BeasSwagger.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BeasConnection>(builder.Configuration.GetSection("Beas:Connection"));
builder.Services.AddControllers().AddJsonOptions(o =>
    o.JsonSerializerOptions.PropertyNamingPolicy = null); // 保留欄位原名(VERSCHN 等)

builder.Services.Configure<BeasScriptOptions>(builder.Configuration.GetSection("Beas:Script"));
// 尚未實作 script 的函數仍走 Mock(只回傳收到的參數)
builder.Services.AddSingleton<IBeasGateway, MockBeasGateway>();
// UseMock=true:只產生 script 不寫庫;false:真的寫 BEAS_COMMON_INPUT
if (builder.Configuration.GetValue<bool>("Beas:UseMock", true))
    builder.Services.AddSingleton<IBeasScriptClient, MockBeasScriptClient>();
else
    builder.Services.AddSingleton<IBeasScriptClient, OdbcBeasScriptClient>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SAP B1 / Beas Test API", Version = "v1",
        Description = "Beas DLL 測試介面。Connection 欄位留空時使用 appsettings 的預設連線。" });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    c.TagActionsBy(a => new[] { a.ActionDescriptor.RouteValues["controller"] });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Beas v1"); c.RoutePrefix = "swagger"; });
app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapControllers();
app.Run();
