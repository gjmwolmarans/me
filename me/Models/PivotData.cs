using me.shared;

namespace me.Models;

/// <summary>
/// Data model for Syncfusion Pivot Table analysis.
/// Represents a flattened view of the relational data structure optimized for pivot operations.
/// </summary>
public class PivotData
{
    /// <summary>
    /// Skill category type (e.g., Skill, Language, Framework, Platform, Tool, API)
    /// </summary>
    public string? TagType { get; set; }
    
    /// <summary>
    /// Specific skill or technology name (e.g., C#, React, Azure)
    /// </summary>
    public string? TagTitle { get; set; }
    
    /// <summary>
    /// Type of learning resource (e.g., Course, Book, Exam, Article)
    /// </summary>
    public string? ResourceType { get; set; }
    
    /// <summary>
    /// Educational content provider (e.g., Pluralsight, Microsoft, Udemy)
    /// </summary>
    public string? Provider { get; set; }
    
    /// <summary>
    /// Professional role associated with the skill (e.g., Software Developer, DevOps Engineer)
    /// </summary>
    public string? RoleTitle { get; set; }
    
    /// <summary>
    /// Resource difficulty/experience level (1-5 scale)
    /// </summary>
    public int ResourceLevel { get; set; }
    
    /// <summary>
    /// Raw experience points calculated as Level × Duration
    /// </summary>
    public double Experience { get; set; }
    
    /// <summary>
    /// Weighted experience considering various factors like recency, relevance, etc.
    /// </summary>
    public double WeightedExperience { get; set; }
    
    /// <summary>
    /// Duration of the learning resource
    /// </summary>
    public TimeSpan Duration { get; set; }
    
    /// <summary>
    /// Count of resources (always 1 for individual records, used for aggregation)
    /// </summary>
    public int ResourceCount { get; set; }
    
    /// <summary>
    /// Name of the specific learning resource
    /// </summary>
    public string? ResourceTitle { get; set; }
}

/// <summary>
/// Extension methods for converting relational data to pivot table format
/// </summary>
public static class PivotDataExtensions
{
    /// <summary>
    /// Transforms the complex relational data structure into a flat collection
    /// suitable for Syncfusion PivotView analysis.
    /// 
    /// This method:
    /// 1. Iterates through all ResourceTag relationships
    /// 2. Resolves foreign key relationships to get related data
    /// 3. Calculates experience metrics
    /// 4. Creates flattened records for pivot analysis
    /// </summary>
    /// <param name="data">The complete data structure with all entities</param>
    /// <returns>Collection of PivotData records ready for pivot table analysis</returns>
    public static IEnumerable<PivotData> ToPivotData(this Data data)
    {
        var pivotData = new List<PivotData>();

        // Process each resource-tag relationship to create pivot records
        foreach (var resourceTag in data.ResourceTags)
        {
            var resource = resourceTag.Resource;
            var tag = resourceTag.Tag;
            var provider = resource?.Provider;
            var role = tag?.Role;

            // Only include records with valid resource and tag data
            if (resource != null && tag != null)
            {
                pivotData.Add(new PivotData
                {
                    // Skill categorization
                    TagType = tag.Type,
                    TagTitle = tag.Title,
                    
                    // Resource information
                    ResourceType = resource.TypeId,
                    ResourceTitle = resource.Title,
                    ResourceLevel = resource.Level,
                    Duration = resource.Duration,
                    
                    // Provider information
                    Provider = provider?.Title ?? "Unknown",
                    
                    // Role association
                    RoleTitle = role?.Title ?? "General",
                    
                    // Experience calculations
                    Experience = resource.Experience,
                    WeightedExperience = resourceTag.WeightedExperience,
                    
                    // Count for aggregations
                    ResourceCount = 1
                });
            }
        }

        return pivotData;
    }
    
    /// <summary>
    /// Gets aggregated statistics from the pivot data for summary reporting
    /// </summary>
    /// <param name="data">The complete data structure</param>
    /// <returns>Summary statistics object</returns>
    public static PivotSummary GetPivotSummary(this Data data)
    {
        var pivotData = data.ToPivotData().ToList();
        
        return new PivotSummary
        {
            TotalSkills = pivotData.Select(p => p.TagTitle).Distinct().Count(),
            TotalCategories = pivotData.Select(p => p.TagType).Distinct().Count(),
            TotalResources = pivotData.Select(p => p.ResourceTitle).Distinct().Count(),
            TotalProviders = pivotData.Select(p => p.Provider).Distinct().Count(),
            TotalRoles = pivotData.Select(p => p.RoleTitle).Distinct().Count(),
            TotalWeightedExperience = pivotData.Sum(p => p.WeightedExperience),
            TotalExperience = pivotData.Sum(p => p.Experience),
            AverageResourceLevel = pivotData.Average(p => p.ResourceLevel),
            TotalDuration = TimeSpan.FromTicks(pivotData.Sum(p => p.Duration.Ticks))
        };
    }
}

/// <summary>
/// Summary statistics for pivot data analysis
/// </summary>
public class PivotSummary
{
    public int TotalSkills { get; set; }
    public int TotalCategories { get; set; }
    public int TotalResources { get; set; }
    public int TotalProviders { get; set; }
    public int TotalRoles { get; set; }
    public double TotalWeightedExperience { get; set; }
    public double TotalExperience { get; set; }
    public double AverageResourceLevel { get; set; }
    public TimeSpan TotalDuration { get; set; }
}