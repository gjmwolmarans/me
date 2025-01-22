using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

using me;
using Syncfusion.Blazor;
using me.Helpers;
using Ganss.XSS;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("${SF_KEY}");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IHttpFactory, HttpFactory>();
builder.Services.Configure<HttpOptions>(options => options.BaseUrl = builder.HostEnvironment.BaseAddress);

builder.Services.AddSingleton<IExcelFileService, ExcelFileService>();

builder.Services.AddFluentUIComponents();

builder.Services.AddSyncfusionBlazor();

builder.Services.AddScoped<IHtmlSanitizer, HtmlSanitizer>(o =>
{
    var sanitizer = new HtmlSanitizer();
    sanitizer.AllowedAttributes.Add("class");
    return sanitizer;
});

var app = builder.Build();

await app.RunAsync();
