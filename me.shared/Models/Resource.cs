﻿using me.shared.Converters;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace me;
public class Resource
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ProviderId { get; set; }
    public string TypeId { get; set; }
    [JsonIgnore]
    public virtual ResourceType Type { get; set; }
    public int Level { get; set; }
    [JsonConverter(typeof(TimeSpanJsonConverter))]
    public TimeSpan Duration { get; set; }
    public string Url { get; set; }
    [NotMapped]
    [JsonIgnore]
    public double Experience => Level * Duration.TotalDays;
    [JsonIgnore]
    public virtual Provider Provider { get; set; }
    [JsonIgnore]
    public virtual ICollection<ResourceTag> ResourceTags { get; set; }
}
