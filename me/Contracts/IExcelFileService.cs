using me.shared;

namespace me;

public interface IExcelFileService
{
    Task<Data> LoadData();
    Task<ICollection<Provider>> GetProvidersAsync();
    Task<ICollection<Resource>> GetResourcesAsync();
    Task<ICollection<Tag>> GetTagsAsync();
    Task<ICollection<ResourceTag>> GetResourceTagsAsync();
}
