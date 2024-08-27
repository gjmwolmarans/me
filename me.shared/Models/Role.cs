using System.Text.Json.Serialization;

namespace me;

public class Role : SelectableBase
{
    public string Title { get; set; }
    [JsonIgnore]
    public virtual ICollection<Tag> Tags { get; set; }
    [JsonIgnore]
    public override bool IsHighlighted => IsSelected;
    [JsonIgnore]
    public double WeightedExperience => Tags.Sum(t => t.WeightedExperience);
}