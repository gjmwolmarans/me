# [gjmwolmarans.me](https://gjmwolmarans.me)

A professional competency matrix and skills analysis platform built with Blazor WebAssembly and Syncfusion components.

## Features

### 📊 Interactive Data Visualization
- **Competency Matrix**: Interactive chart displaying skills across different professional roles
- **Syncfusion Pivot Table**: Advanced data analysis with dynamic grouping and filtering
- **Skills Analysis**: Dedicated page for comprehensive skill distribution analysis

### 🎯 Core Components

#### Syncfusion Pivot Table Implementation
The application features a fully integrated Syncfusion Pivot Table for advanced data analysis:

- **Location**: `/me/Components/Graph/PivotTable.razor`
- **Data Model**: `/me/Models/PivotData.cs`
- **Integration**: Available in both Competency Matrix (tabbed view) and dedicated Skills Analysis page
- **Features**:
  - Dynamic grouping by skill categories and resource types
  - Weighted experience calculations
  - Interactive field list and grouping bar
  - Export capabilities
  - Responsive design

#### Navigation
- **Main Page**: `/` - Competency Matrix with chart and pivot table tabs
- **Skills Analysis**: `/pivot-analysis` - Dedicated pivot table analysis page

### 🔧 Technical Architecture

#### Data Models
- **Resources**: Learning materials (courses, books, exams) with duration, level, and experience metrics
- **Tags**: Skill categories (Skills, Languages, Frameworks, Platforms, Tools, APIs)
- **ResourceTags**: Junction table linking resources to tags with weighted experience
- **Roles**: Professional roles grouping related skills
- **Providers**: Educational content providers (Pluralsight, Microsoft, etc.)

#### Key Technologies
- **Blazor WebAssembly** (.NET 8)
- **Syncfusion Blazor Components** (Charts, PivotView, Spinner)
- **Microsoft FluentUI** (UI Components)
- **JSON Data Source** with custom converters

#### Deployment
- **GitHub Actions** automated deployment to GitHub Pages
- **Syncfusion Licensing** via repository secrets (`SF_KEY`)
- **Static hosting** optimized for WebAssembly

### 📈 Pivot Table Configuration

The pivot table is optimally configured for skills analysis:

```csharp
// Rows: Hierarchical skill categorization
TagType (Category) → TagTitle (Specific Tag)

// Columns: Resource organization  
ResourceType → Provider

// Values: Experience metrics
- Weighted Experience (Sum)
- Resource Count (Count) 
- Total Experience (Sum)

// Filters: Role-based filtering
- Role Title
- Resource Level
```

### 🚀 Getting Started

1. **Clone the repository**
2. **Set up Syncfusion license**: Add `SF_KEY` repository secret
3. **Build and run**: `dotnet run --project me/me.csproj`
4. **Access locally**: Navigate to the development server URL

### 📖 Documentation
- See `/docs/PIVOT_TABLE.md` for detailed pivot table implementation guide
- Component documentation available in source files

# Resources & Helpful links
- [Blazorwasm: Using AddHttpClient with Singleton does not do what you think](https://tomkarho.com/blog/post/blazorwasm:-using-addhttpclient-with-singleton-does-not-do-what-you-think)
- [Syncfusion Blazor Pivot Table Documentation](https://blazor.syncfusion.com/documentation/pivot-table/getting-started)