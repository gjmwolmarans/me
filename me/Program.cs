using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Syncfusion.Blazor;
using me;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzI2ODUyNEAzMjM1MmUzMDJlMzBYcUlHTFEvamlaZzVYeTBtTWxEVkM4K0JKREFNSHZnazhKSUlJZkZaYWRJPQ==");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddFluentUIComponents();
builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();
