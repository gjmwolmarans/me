using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using me.console.Contracts;
using me.console.Services;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IExcelFileService, ExcelFileService>();

using IHost host = builder.Build();

RunPipeline(host.Services);

await host.RunAsync();

static void RunPipeline(IServiceProvider hostProvider)
{
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    var excelFileService = provider.GetRequiredService<IExcelFileService>();

    Console.WriteLine("...");

    var resources = excelFileService.GetResources();

    var resourcesByProvider = resources.GroupBy(r => r.Provider)
        .OrderByDescending(kv => kv.Sum(v => v.Experience));

    foreach (var group in resourcesByProvider)
    {
        Console.WriteLine($"{group.Key.Title} ({group.Count()}) {group.Sum(r => r.Duration.TotalHours)} {group.Sum(r => r.Experience)}");
    }

    Console.WriteLine();
}