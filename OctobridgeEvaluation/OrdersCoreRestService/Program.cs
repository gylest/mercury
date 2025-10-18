namespace OctobridgeCoreRestService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // CORS policy
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:3000", "http://localhost:5000")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        // Health Checks
        builder.Services.AddHealthChecks()
            .AddSqlServer(builder.Configuration.GetConnectionString("OctobridgeDatabase"));

        // Controllers with custom convention
        builder.Services.AddControllers(options =>
        {
            options.Conventions.Add(new GroupingByNamespaceConvention());
        });

        // Database Connection
        builder.Services.AddDbContext<OctobridgeContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("OctobridgeDatabase")));

        // Register the Swagger Generator (OpenAPI)
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Octobridge Core API", Description = "REST API for Octobridge database", Version = "v1" });
            c.SwaggerDoc("v2", new OpenApiInfo { Title = "Octobridge Core API", Description = "REST API for Octobridge database", Version = "v2" });
            c.OperationFilter<RemoveVersionParameterFilter>();
            c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
        });

        // API Versioning
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        // Basic Authentication
        builder.Services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

        // Add UserService for dependency injection
        builder.Services.AddScoped<IUserService, UserService>();

        var app = builder.Build();

        // Error page and HSTS
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        // Enable OpenAPI/Swagger
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Octobridge Core API V1");
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "Octobridge Core API V2");
        });

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapHealthChecks("/health");

        app.Run();
    }

    // Custom conventions and filters
    public class GroupingByNamespaceConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace;
            var apiVersion = controllerNamespace.Split(".").Last().ToLower();
            if (!apiVersion.StartsWith("v")) { apiVersion = "v1"; }
            controller.ApiExplorer.GroupName = apiVersion;
        }
    }

    public class RemoveVersionParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.FirstOrDefault(p => p.Name == "version");
            if (versionParameter != null)
                operation.Parameters.Remove(versionParameter);
        }
    }

    public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                var newKey = path.Key.Contains("v{version}")
                    ? path.Key.Replace("v{version}", swaggerDoc.Info.Version)
                    : path.Key;
                paths.Add(newKey, path.Value);
            }
            swaggerDoc.Paths = paths;
        }
    }
}
