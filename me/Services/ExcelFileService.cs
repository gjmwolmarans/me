
using System.Net.Http.Json;
using System.Text.Json;
using me.shared;

namespace me;

public class ExcelFileService : IExcelFileService
{
    private readonly HttpClient _httpClient;
    Data Data { get; set; } = new Data();

    public ExcelFileService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        //foreach (var provider in Data.Providers)
        //{
        //    provider.Resources = Data.Resources.Where(r => r.ProviderId == provider.Id).ToList();
        //}
        //foreach (var resource in Data.Resources)
        //{
        //    resource.Provider = Data.Providers.FirstOrDefault(p => p.Id == resource.ProviderId);
        //    resource.ResourceTags = Data.ResourceTags.Where(rt => rt.ResourceId == resource.Id).ToList();
        //}
        //foreach (var resourceTag in Data.ResourceTags)
        //{
        //    resourceTag.Resource = Data.Resources.FirstOrDefault(r => r.Id == resourceTag.ResourceId);
        //    resourceTag.Tag = Data.Tags.FirstOrDefault(t => t.Id == resourceTag.TagId);
        //}
        //foreach (var tag in Data.Tags)
        //{
        //    tag.ResourceTags = Data.ResourceTags.Where(rt => rt.TagId == tag.Id).ToList();
        //}
    }

    public async Task<Data> LoadData()
    {
        var response = await _httpClient.GetFromJsonAsync<Data>("data/data.json");

        if (Data == null)
        {
            throw new Exception("Failed to deserialize data.json");
        }

        return response;
    }

    public async Task<ICollection<Provider>> GetProvidersAsync()
    {
        return Data.Providers.ToList();
    }

    public async Task<ICollection<Resource>> GetResourcesAsync()
    {
        return Data.Resources.ToList();
    }

    public async Task<ICollection<ResourceTag>> GetResourceTagsAsync()
    {
        return Data.ResourceTags.ToList();
    }

    public async Task<ICollection<Tag>> GetTagsAsync()
    {
        return Data.Tags.ToList();
    }
}