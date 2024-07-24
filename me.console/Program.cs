using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using me.console.Contracts;
using me.console.Services;
using System.Text.Json;
using me.shared;
using me.shared.Converters;

string _competencyMatrixPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CompetencyMatrix.xlsx");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IExcelFileService, ExcelFileService>(provider =>
{
    DownloadCompetencyMatrix(_competencyMatrixPath).Wait();

    return new ExcelFileService(_competencyMatrixPath);
});

using IHost host = builder.Build();

await RunPipeline(host.Services);

static async Task RunPipeline(IServiceProvider hostProvider)
{
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    var excelFileService = provider.GetRequiredService<IExcelFileService>();

    Console.WriteLine("...");

    var providers = excelFileService.GetProviders();
    var resources = excelFileService.GetResources();
    var tags = excelFileService.GetTags();
    var resourceTags = excelFileService.GetResourceTags();

    var data = new Data {
        Providers = providers,
        Resources = resources,
        Tags = tags,
        ResourceTags = resourceTags
    };

    await ExportToJson(data, Path.Combine("../me/wwwroot/data/", $"{nameof(data)}.json"));


    Console.WriteLine("Done Exporting Resources to JSON...");

    Console.WriteLine();
}


static async Task DownloadCompetencyMatrix(string path)
{
    var url = new Uri("https://onedrive.live.com/download.aspx?resid=F9480B74B852B3B7!251307&ithint=file,xlsx&authkey=!ABBznQ1L9IJP6BQ");

    using HttpClient client = new HttpClient();

    using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

    using var stream = await response.Content.ReadAsStreamAsync();

    using var fileStream = File.Create(path);

    await stream.CopyToAsync(fileStream);
}



static async Task ExportToJson<T>(T data, string path)
{
    string json = JsonSerializer.Serialize(data, new JsonSerializerOptions {
        WriteIndented = false,
        Converters =
        {
            new TimeSpanJsonConverter()
        }
    });
    await File.WriteAllTextAsync(path, json);
}
