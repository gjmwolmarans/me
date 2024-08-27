using System.Text.Json.Serialization;

namespace me.shared;

public class Data
{
    public IEnumerable<Provider> Providers { get; set; } = Enumerable.Empty<Provider>().ToList();
    public IEnumerable<Resource> Resources { get; set; } = Enumerable.Empty<Resource>().ToList();
    public IEnumerable<Tag> Tags { get; set; } = Enumerable.Empty<Tag>().ToList();
    public IEnumerable<ResourceTag> ResourceTags { get; set; } = Enumerable.Empty<ResourceTag>().ToList();
    [JsonIgnore]
    public IEnumerable<Role> Roles { get; set; } = Enumerable.Empty<Role>().ToList();
}
