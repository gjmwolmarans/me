namespace me.console.Contracts;

public interface IExcelFileService
{
    ICollection<Provider> GetProviders();
    ICollection<Resource> GetResources();
    ICollection<Tag> GetTags();
    ICollection<ResourceTag> GetResourceTags();
}
