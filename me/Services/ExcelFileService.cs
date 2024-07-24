using System.Net.Http.Json;
using System.Text.Json;
using me.Helpers;
using me.shared;
using me.shared.Converters;

namespace me;

public class ExcelFileService : IExcelFileService
{
    private readonly HttpClient _httpClient;
    private static Data? Data { get; set; } = null;

    public ExcelFileService(IHttpFactory httpFactory)
    {
        _httpClient = httpFactory.GetClient();
    }

    public async Task<Data> LoadData()
    {
        if (Data == null)
        {
            Data = await _httpClient.GetFromJsonAsync<Data>("data/data.json", new JsonSerializerOptions
            {
                Converters = { new TimeSpanJsonConverter() }
            });
        }

        return Data;
    }

    public async Task<ICollection<Provider>> GetProvidersAsync()
    {
        if (Data == null) await LoadData();
        return Data.Providers.ToList();
    }

    public async Task<ICollection<Resource>> GetResourcesAsync()
    {
        if (Data == null) await LoadData();
        return Data.Resources.ToList();
    }

    public async Task<ICollection<ResourceTag>> GetResourceTagsAsync()
    {
        if (Data == null) await LoadData();
        return Data.ResourceTags.ToList();
    }

    public async Task<ICollection<Tag>> GetTagsAsync()
    {
        if (Data == null) await LoadData();
        return Data.Tags.ToList();
    }
}