using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

using me;
using Syncfusion.Blazor;
using me.Helpers;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzM4OTY5NEAzMjM2MmUzMDJlMzBJTkpwTk8zeWNKMXJPVzJkZ0l2U0lndUp6cmlhZ28wRWJuSGFMdE1KN29JPQ==");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IHttpFactory, HttpFactory>();
builder.Services.Configure<HttpOptions>(options => options.BaseUrl = builder.HostEnvironment.BaseAddress);

builder.Services.AddSingleton<IExcelFileService, ExcelFileService>();

builder.Services.AddFluentUIComponents();

builder.Services.AddSyncfusionBlazor();

var app = builder.Build();

await app.RunAsync();
