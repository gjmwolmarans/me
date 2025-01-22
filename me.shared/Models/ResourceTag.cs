using System.Text.Json.Serialization;

namespace me;

public class ResourceTag
{
    public int ResourceId { get; set; }
    public int TagId { get; set; }
    [JsonIgnore]
    public virtual Resource Resource { get; set; }
    [JsonIgnore]
    public virtual Tag Tag { get; set; }
    [JsonIgnore]
    public double WeightedExperience
    {
        get
        {
            var taggedResourceTags = Resource.ResourceTags.Where(rt => rt.Tag.Type == Tag.Type);
            return Resource.Experience / taggedResourceTags.Count();
        }
    }
}
