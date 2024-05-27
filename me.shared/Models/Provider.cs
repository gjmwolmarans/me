using System.Text.Json.Serialization;

namespace me;

public class Provider
{
    public string Id { get; set; }
    public string Title { get; set; }

    [JsonIgnore]
    public virtual ICollection<Resource> Resources { get; set; }
}
