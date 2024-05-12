namespace me;

public class Provider
{
    public string Id { get; set; }
    public string Title { get; set; }

    public virtual ICollection<Resource> Resources { get; set; }
}
