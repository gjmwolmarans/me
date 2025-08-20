namespace me.Models;

public class ChartData
{
    public string? Role { get; init; }
    public IEnumerable<Tag> Tags { get; init; }
    public IEnumerable<Tag> HighlightedTags => Tags.Any(tag => tag.IsHighlighted) ? Tags.Where(tag => tag.IsHighlighted) : Tags;
    public double WeightedExperience => HighlightedTags.Sum(tag => tag.WeightedExperience);
}

public class Chart3DData : ChartData
{
    public double Skill => HighlightedTags.Where(tag => tag.Type == "Skill").Sum(tag => tag.WeightedExperience);
    public double Language => HighlightedTags.Where(tag => tag.Type == "Language").Sum(tag => tag.WeightedExperience);
    public double Framework => HighlightedTags.Where(tag => tag.Type == "Framework").Sum(tag => tag.WeightedExperience);
    public double Platform => HighlightedTags.Where(tag => tag.Type == "Platform").Sum(tag => tag.WeightedExperience);
    public double Tool => HighlightedTags.Where(tag => tag.Type == "Tool").Sum(tag => tag.WeightedExperience);
    public double Api => HighlightedTags.Where(tag => tag.Type == "API").Sum(tag => tag.WeightedExperience);
}