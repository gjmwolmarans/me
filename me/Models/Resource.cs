using System.ComponentModel.DataAnnotations.Schema;

namespace me;

public class Resource
{
    public int Id { get; set; }
    public string Title { get; set;}
    public string ProviderId { get; set; }
    public string Type { get; set; }
    public int Level { get; set; }
    public TimeSpan Duration { get; set; }
    public string Url { get; set; }
    [NotMapped]
    public double Experience => Level * Duration.TotalDays;
    public virtual Provider Provider { get; set; }
    public virtual ICollection<ResourceTag> ResourceTags { get; set; }
}
