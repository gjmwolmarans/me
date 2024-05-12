using Microsoft.EntityFrameworkCore;
using Syncfusion.XlsIO;
using System.Reflection;

namespace me;

public class ExcelFileService : IExcelFileService, IDisposable
{
    MeDbContext _dbContext;

    Stream Stream { get; set; }
    ExcelEngine ExcelEngine { get; set; }
    IWorkbook Workbook { get; set; }

    public ExcelFileService(MeDbContext dbContext)
    {
        _dbContext = dbContext;

        LoadWorkbook();
    }

    public void LoadWorkbook()
    {
        ExcelEngine = new ExcelEngine();

        var application = ExcelEngine.Excel;
        application.DefaultVersion = ExcelVersion.Excel2016;

        Stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("me.Resources.CompetencyMatrix.xlsx")!;

        Workbook = application.Workbooks.Open(Stream, ExcelOpenType.Automatic);
    }

    public async Task<ICollection<Provider>> GetProvidersAsync()
    {
        if (!_dbContext.Providers.Any())
        {
            await _dbContext.Providers.AddRangeAsync(
                LoadTable("Providers", rowCells => {
                    return new Provider
                    {
                        Id = rowCells[0].DisplayText,
                        Title = rowCells[1].DisplayText,
                    };
                })
            );

            await _dbContext.SaveChangesAsync();
        }

        return await _dbContext.Providers.ToListAsync();
    }

    public async Task<ICollection<Resource>> GetResourcesAsync()
    {
        if (!_dbContext.Resources.Any())
        {
            await _dbContext.Resources.AddRangeAsync(
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

            await _dbContext.SaveChangesAsync();
        }

        return await _dbContext.Resources.ToListAsync();
    }
    
    public async Task<ICollection<Tag>> GetTagsAsync()
    {
        if (!_dbContext.Tags.Any())
        {
            await _dbContext.Tags.AddRangeAsync(
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

            await _dbContext.SaveChangesAsync();
        }

        return await _dbContext.Tags.ToListAsync();
    }

    public async Task<ICollection<ResourceTag>> GetResourceTagsAsync()
    {
        if (!_dbContext.ResourceTags.Any())
        {
            await _dbContext.ResourceTags.AddRangeAsync(
                LoadTable("ResourceTags", rowCells => {
                    return new ResourceTag
                    {
                        ResourceId = int.Parse(rowCells[0].DisplayText),
                        TagId = int.Parse(rowCells[2].DisplayText),
                    };
                })
            );

            await _dbContext.SaveChangesAsync();
        }

        return await _dbContext.ResourceTags.ToListAsync();
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
