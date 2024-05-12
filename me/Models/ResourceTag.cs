namespace me;

public class ResourceTag
{
    public int ResourceId { get; set; }
    public int TagId { get; set; }
    public virtual Resource Resource { get; set; }
    public virtual Tag Tag { get; set; }
}
