using System.Text.Json.Serialization;

namespace me;

public class Tag : SelectableBase
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public string? RoleId { get; set; }
    [JsonIgnore]
    public string Name => $"{Type} - {Role?.Title}";
    [JsonIgnore]
    public virtual Role? Role { get; set; }
    [JsonIgnore]
    public virtual ICollection<ResourceTag>? ResourceTags { get; set; }
    [JsonIgnore]
    public override bool IsHighlighted => Role?.IsSelected == true || IsSelected;
    public double WeightedExperience => ResourceTags?.Sum(rt => rt.Resource.Experience) ?? 0;
}
