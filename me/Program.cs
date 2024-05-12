using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Syncfusion.Blazor;
using me;
using Microsoft.EntityFrameworkCore;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzI2ODUyNEAzMjM1MmUzMDJlMzBYcUlHTFEvamlaZzVYeTBtTWxEVkM4K0JKREFNSHZnazhKSUlJZkZaYWRJPQ==");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddDbContext<MeDbContext>(options =>
    options.UseInMemoryDatabase("Me"));

builder.Services.AddSingleton<MeDbContext>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton(sp => new ExcelFileService());

builder.Services.AddFluentUIComponents();
builder.Services.AddSyncfusionBlazor();

var app = builder.Build();



app.RunAsync();
