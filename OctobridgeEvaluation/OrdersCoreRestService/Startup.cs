using System.Linq;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OctobridgeCoreRestService.Helpers;
using OctobridgeCoreRestService.Models;
using OctobridgeCoreRestService.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OctobridgeCoreRestService
{
    /// <summary>
    /// 
    /// HTTP Client Testing
    /// 
    /// https://localhost:44366/health                                               Health Check (returns Healthy/Unhealthy)
    ///
    /// </summary>
    /// 
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

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // CORS policy
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000", "http://localhost:5000") // React development/release
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            // Health Checks
            services.AddHealthChecks()                                                          // Basic health test    - is service running?
                    .AddSqlServer(Configuration.GetConnectionString("OctobridgeDatabase"));     // Database health test - is database available?

            // Controllers
            services.AddControllers(options =>
            {
                options.Conventions.Add(new GroupingByNamespaceConvention());
            });

            // Database Connection
            services.AddDbContext<OctobridgeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OctobridgeDatabase")));

            // Register the Swagger Generator
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Octobridge Core API", Description = "REST API for Octobridge database", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Octobridge Core API", Description = "REST API for Octobridge database", Version = "v2" });
                c.OperationFilter<RemoveVersionParameterFilter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
            });

            services.AddApiVersioning(options =>
              {
                  options.DefaultApiVersion = new ApiVersion(1, 0);
                  options.AssumeDefaultVersionWhenUnspecified = true;
                  options.ReportApiVersions = true;
              }).AddApiExplorer(options =>
              {
                  options.GroupNameFormat = "'v'VVV";
                  options.SubstituteApiVersionInUrl = true;
              });


            // Basic Authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // Add UserService for dependency injection
            services.AddScoped<IUserService, UserService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Octobridge Core API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Octobridge Core API V2");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();    // This must come before UseEndPoints()!
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

        }
    }
}
