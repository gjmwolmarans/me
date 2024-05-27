namespace me.shared;

public class Data
{
    public IEnumerable<Provider> Providers { get; set; } = Enumerable.Empty<Provider>();
    public IEnumerable<Resource> Resources { get; set; } = Enumerable.Empty<Resource>(); 
    public IEnumerable<Tag> Tags { get; set; } = Enumerable.Empty<Tag>();
    public IEnumerable<ResourceTag> ResourceTags { get; set; } = Enumerable.Empty<ResourceTag>();
}
