# ASP.NET MVC Developer Notes

## Create Application

1. Add New Project to solution:-
ASP.NET Core Web Application ,type = Web Application (Model-View-Controller)

2. Install Entity Framework Core NuGet Packages
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Design

3. Reverse Engineer Existing Database

* Open Package Manger Console:-
Tools –> NuGet Package Manager –> Package Manager Console
(HINT: Ensure Default Project is set correctly)

* Run this command to create Model and database context:-  
```Scaffold-DbContext "Server=TONY23-PC\\DEVSQL;Database=Octobridge;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models```

* If the database model changes then use force flag to regenerate existing and new objects  
```Scaffold-DbContext "Server=TONY23-PC\\DEVSQL;Database=Octobridge;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force```
(IMPORTANT NOTE: This will remove any custom data annotations)

* This creates the classes :
   * Attachment.cs
   * CodedValue.cs
   * Customer.cs
   * OctobridgeContext.cs
   * Order.cs
   * OrderAudit.cs
   * Product.cs

4. Register the database context
* IMPORTANT: Services (such as the EF Core DB context) must be registered with DI during application startup.
* In appsettings.json add a database connection string for Octobridge.
* In Startup.ConfigureServices, add the following:-
   * ```services.AddDbContext<OctobridgeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OctobridgeDatabase")));```

5. Add Controller (with views) for Order

* Select Controllers folder, right click and select "Add Controller".
* Select the following controller type: "MVC Controller with Views using Entity Framework"
* In the dialog box select the following:-
    * Model Class                = Order
    * Data context class         = OctobridgeContext
    * Generate Views             = checked
    * Reference script libraries = checked
    * Use a layout page          = checked

6. Add Order to Navigation Bar

* Edit the following file: ```_Layout.cshtml```  
* Add new unordered list item of class "nav-item"

7. Add Order Information to Home Page

* Edit the following file: ```Index.cshtml```  
* Add new unordered list item

---

## Logging

Logging provided using "Microsoft.Extensions.Logging" NuGet package.

Three stages:-
1) Call ConfigureLogging() in program.cs
2) Use dependency injection to add logger to required class
3) Make log calls using LogWarning() and LogInformation()

Log Output:-
* Console  = Web Server console (logs for Information and above)
* EventLog = Windows Application Log with source = .NET Runtime (logs for Warning and above)