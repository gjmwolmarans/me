using System.Text.Json.Serialization;

namespace me;

public class ResourceType : SelectableBase
{
    public string Title { get; set; }
    [JsonIgnore]
    public virtual ICollection<Resource> Resources { get; set; }
    [JsonIgnore]
    public override bool IsHighlighted => IsSelected;
    [JsonIgnore]
    public double WeightedExperience => Resources.Sum(t => t.Experience);
}
