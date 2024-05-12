namespace me;

public interface IExcelFileService
{
    Task<ICollection<Provider>> GetProvidersAsync();
    Task<ICollection<Resource>> GetResourcesAsync();
    Task<ICollection<Tag>> GetTagsAsync();
    Task<ICollection<ResourceTag>> GetResourceTagsAsync();
}
