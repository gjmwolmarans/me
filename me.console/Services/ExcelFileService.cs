using me.console.Contracts;
using Syncfusion.XlsIO;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace me.console.Services;

public class ExcelFileService : IExcelFileService, IDisposable
{
    public List<Provider> Providers { get; set; } = new List<Provider>();
    public List<Resource> Resources { get; set; } = new List<Resource>();
    public List<Tag> Tags { get; set; } = new List<Tag>();
    public List<ResourceTag> ResourceTags { get; set; } = new List<ResourceTag>();

    Stream Stream { get; set; }
    ExcelEngine ExcelEngine { get; set; }
    IWorkbook Workbook { get; set; }

    public ExcelFileService(string path)
    {
        LoadWorkbook(path);
        LoadTables();
    }

    private void LoadTables()
    {
        GetProviders();
        GetResources();
        GetTags();
        GetResourceTags();
    }

    private void LoadWorkbook(string path)
    {
        ExcelEngine = new ExcelEngine();

        var application = ExcelEngine.Excel;
        application.DefaultVersion = ExcelVersion.Excel2016;

        Stream = new FileStream(path, FileMode.Open, FileAccess.Read);

        Workbook = application.Workbooks.Open(Stream, ExcelOpenType.Automatic);
    }

    public ICollection<Provider> GetProviders()
    {
        if (!Providers.Any())
        {
            Providers.AddRange(
                LoadTable("Providers", rowCells => {
                    return new Provider
                    {
                        Id = rowCells[0].DisplayText,
                        Title = rowCells[1].DisplayText,
                    };
                })
            );
        }

        return Providers;
    }

    public ICollection<Resource> GetResources()
    {
        if (!Resources.Any())
        {
            Resources.AddRange(
                LoadTable("Resources", rowCells => {
                    return new Resource
                    {
                        Id = int.Parse(rowCells[0].DisplayText),
                        Title = rowCells[1].DisplayText,
                        ProviderId = rowCells[2].DisplayText,
                        Type = rowCells[3].DisplayText,
                        Level = int.Parse(rowCells[4].DisplayText),
                        Duration = TimeSpan.Parse(rowCells[5].DisplayText),
                        Url = rowCells[7].DisplayText
                    };
                })
            );

        }

        return Resources;
    }
    
    public ICollection<Tag> GetTags()
    {
        if (!Tags.Any())
        {
            Tags.AddRange(
                LoadTable("Tags", rowCells => {
                    return new Tag
                    {
                        Id = int.Parse(rowCells[0].DisplayText),
                        Title = rowCells[1].DisplayText,
                        Type = rowCells[2].DisplayText,
                        Role = rowCells[3].DisplayText
                    };
                })
            );
        }

        return Tags;
    }

    public ICollection<ResourceTag> GetResourceTags()
    {
        if (!ResourceTags.Any())
        {
            ResourceTags.AddRange(
                LoadTable("ResourceTags", rowCells => {
                    return new ResourceTag
                    {
                        ResourceId = int.Parse(rowCells[0].DisplayText),
                        TagId = int.Parse(rowCells[2].DisplayText),
                    };
                })
            );
        }

        return ResourceTags;
    }

    private ICollection<T> LoadTable<T>(string worksheetName, Func<IRange[], T> constructor)
    {
        var result = new List<T>();

        var worksheet = Workbook.Worksheets.FirstOrDefault(w => w.Name == worksheetName);

        if (worksheet == null)
        {
            return result;
        }

        var table = worksheet.ListObjects[0];
        var tableRange = table.Location;
        var headerRow = true;
        foreach (var row in tableRange.Rows)
        {
            if (headerRow)
            {
                headerRow = false;
                continue;
            }

            var rowObject = constructor(row.Cells);
            result.Add(rowObject);
        }

        return result;
    }
    
    public void Dispose()
    {
        ExcelEngine.Dispose();
        Stream.Dispose();
    }
}
