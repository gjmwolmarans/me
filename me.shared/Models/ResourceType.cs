using System.Text.Json.Serialization;

namespace me;

public class ResourceType : SelectableBase
{
    [JsonIgnore]
    public virtual ICollection<Resource> Resources { get; set; }
    [JsonIgnore]
    public override bool IsHighlighted => IsSelected;
    [JsonIgnore]
    public double WeightedExperience => Resources.Sum(t => t.Experience);
}
