using me.Helpers;
using me.shared;
using me.shared.Converters;
using System.Net.Http.Json;
using System.Text.Json;

namespace me;

public class ExcelFileService : IExcelFileService
{
    private readonly HttpClient _httpClient;
    private static Data? Data { get; set; } = null;

    private static ICollection<Role> Roles { get; set; } = new List<Role>();

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

            foreach (var resourceTag in Data.ResourceTags)
            {
                resourceTag.Resource = Data.Resources.FirstOrDefault(r => r.Id == resourceTag.ResourceId);
                resourceTag.Tag = Data.Tags.FirstOrDefault(t => t.Id == resourceTag.TagId);
            }

            foreach (var resource in Data.Resources)
            {
                resource.Provider = Data.Providers.FirstOrDefault(p => p.Id == resource.ProviderId);
                resource.ResourceTags = Data.ResourceTags.Where(rt => rt.ResourceId == resource.Id).ToList();
            }

            foreach (var tag in Data.Tags)
            {
                tag.ResourceTags = Data.ResourceTags.Where(rt => rt.TagId == tag.Id).ToList();

            }

            Data.Roles = Data.Tags.Select(t => t.RoleId).Distinct().Select(r => new Role { Title = r }).ToList();
            foreach (var role in Data.Roles)
            {
                role.Tags = Data.Tags.Where(t => t.RoleId == role.Title).ToList();
                foreach (var tag in role.Tags)
                {
                    tag.Role = role;
                }
            }
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

    public async Task<ICollection<Role>> GetRolesAsync()
    {
        if (Data == null) await LoadData();
        return Roles;
    }
}