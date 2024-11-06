using System.Text.Json.Serialization;

namespace me;
public abstract class SelectableBase
{
    public string Title { get; set; }
    [JsonIgnore]
    public bool IsSelected { get; set; }
    [JsonIgnore]
    public abstract bool IsHighlighted { get; }
}