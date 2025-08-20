using System.Text.Json.Serialization;

namespace me;

public class Role : SelectableBase
{
    [JsonIgnore]
    public virtual ICollection<Tag> Tags { get; set; }
    [JsonIgnore]
    public override bool IsHighlighted => IsSelected || Tags.Any(tag => tag.IsSelected);
    [JsonIgnore]
    public double WeightedExperience => Tags.Sum(t => t.WeightedExperience);
}