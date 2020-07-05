### Vscode Extensions Used

- C# (intellisense)
- c# Extensions (to add classes)
- Nuget package manager

### Remove HTTPS for dev server in launchSettings.json

 <pre><code>
     "applicationUrl": "https://localhost:5001;http://localhost:5000",
          "applicationUrl": "http://localhost:5000",
 </code></pre>

### remove from startup class

// app.UseHttpsRedirection();

### Adding New Models Folder

Models>create class>URLShort

### Nuget Add Entitiy Framework Core

Nuget Packakge Manager
Microoft.EntityFrameworkCore

- Create DBContext Class inherit from DbContext
- add ctor with options and pass options to base class
- add DbSet
- add services to Startup class and provide conn string

Microsoft.EntityFrameworkCore.SqlServer

## Adding Connection String on appsettings.json file

     <pre><code>
     "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=URLShorten;Trusted_Connection=True;"
     },
     </code></pre>

## Adding Migration

##### Check syntac using

dotnet ef migrations -h

- Note: if you have dotnet run c running you need to stop for migrations

#### If it throws error for missing Design of EF then add belwo nuget

Add Nuget Package
Add Microsoft.EntityFrameworkCore.Design

### Add new migration

    dotnet ef migrations add InitialCreate

- Note: if ef tools vs the ef design version dont match update ef tools using below cmd in cmd line
  dotnet tool update --global dotnet-ef

Migration folder is created with scripts
dotnet ef -h
dotnet ef database -h

### Update Database

- update database, create a database and tables
  dotnet ef database update

### Repository Pattern and DI

- Create Interface in Data Folder #IURLRepository
- Create class to implement the interface
- public class URLRepository : IURLRepository

### Creating DTO

Create DTO folder >ADD DTO class

### Adding Automapper

- Add Nuget Package
  AutoMapper.Extensions.Microsoft.DependencyInjection
- Create a AutoMapperProfile class
- Add automapper as a service in configure class
  services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //provide assembly where you willfind automapper profile class

### Adding Controller Class

- Add Controller
- Add ctor with repo, config and mapper

 <pre><code>

  private readonly IURLRepository \_repo;
  private readonly IConfiguration \_config;
  private readonly IMapper \_mapper;

          public URLController(IURLRepository repo, IConfiguration config, IMapper mapper)
          {
              this._mapper = mapper;
              this._config = config;
              this._repo = repo;

          }
  </code></pre>

### Adding cors headers

Add after UseRoutng

  <pre><code>
    app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
  </code></pre>

### Adding Newtonsoft JSON

- Nugut Package add
  Microsoft.AspNetCore.Mvc.NewtonsoftJson

-Add services

  <pre><code>
services.AddControllers().AddNewtonsoftJson(opt =>
{
opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}); //replaces system.text.json
  </code></pre>
