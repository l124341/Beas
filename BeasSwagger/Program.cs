using System.Reflection;
using BeasSwagger.Models;
using BeasSwagger.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<BeasConnection>(builder.Configuration.GetSection("Beas:Connection"));
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = null; // 保留欄位原名(VERSCHN 等)
    o.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
builder.Services.Configure<BeasOptions>(builder.Configuration.GetSection("Beas"));
builder.Services.AddSingleton<BeasScriptRunner>();
builder.Services.AddSingleton<BeasResultReader>();

builder.Services.Configure<BeasScriptOptions>(builder.Configuration.GetSection("Beas:Script"));
builder.Services.Configure<BeasBslOptions>(builder.Configuration.GetSection("Beas:Bsl"));
// 尚未實作 script 的函數仍走 Mock(只回傳收到的參數)
builder.Services.AddSingleton<IBeasGateway, MockBeasGateway>();
// UseMock=true:只產生 script 不寫庫;false:真的寫 BEAS_COMMON_INPUT
if (builder.Configuration.GetValue<bool>("Beas:UseMock", true))
{
    builder.Services.AddSingleton<IBeasScriptClient, MockBeasScriptClient>();
    builder.Services.AddSingleton<IBeasQueryClient, MockBeasQueryClient>();
    builder.Services.AddSingleton<IBeasCommandClient, MockBeasCommandClient>();
    builder.Services.AddSingleton<IBeasBslHttpClient, MockBeasBslHttpClient>();
}
else
{
    builder.Services.AddSingleton<IBeasScriptClient, OdbcBeasScriptClient>();
    builder.Services.AddSingleton<IBeasQueryClient, OdbcBeasQueryClient>();
    builder.Services.AddSingleton<IBeasCommandClient, OdbcBeasCommandClient>();
    // Interface=BslHttp:直接呼叫 BSL。Cookie 由程式自行帶 beas-sessionid,所以關閉自動 Cookie。
    builder.Services.AddHttpClient<IBeasBslHttpClient, BeasBslHttpClient>((sp, c) =>
            c.Timeout = TimeSpan.FromSeconds(Math.Max(1, sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<BeasBslOptions>>().Value.TimeoutSeconds)))
        .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler { UseCookies = false, PooledConnectionLifetime = TimeSpan.FromMinutes(5) });
}

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
app.Use(async (ctx, next) =>
{
    try { await next(); }
    catch (BeasInterfaceNotSupportedException e) { ctx.Response.StatusCode = 501; await ctx.Response.WriteAsJsonAsync(new BeasResult(false, e.Message)); }
    catch (BeasDirectSqlDisabledException e) { ctx.Response.StatusCode = 403; await ctx.Response.WriteAsJsonAsync(new BeasResult(false, e.Message)); }
    catch (ArgumentException e) { ctx.Response.StatusCode = 400; await ctx.Response.WriteAsJsonAsync(new BeasResult(false, e.Message)); }
});
app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapControllers();
app.Run();
