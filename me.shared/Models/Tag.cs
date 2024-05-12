namespace me;

public class Tag
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Role { get; set; }
    public virtual ICollection<ResourceTag> ResourceTags { get; set; }
}
