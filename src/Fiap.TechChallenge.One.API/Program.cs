using Asp.Versioning;
using Fiap.TechChallenge.One.API.Extensions;
using Fiap.TechChallenge.One.API.Infrastructure;
using System.Reflection;
using Fiap.TechChallenge.One.Application;
using Fiap.TechChallenge.One.Infrastructure;
using Asp.Versioning.Builder;
using Asp.Versioning.ApiExplorer;
using Fiap.TechChallenge.One.API.OpenApi;
using Prometheus;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                      });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.UseHttpClientMetrics();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

WebApplication app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoints(versionedGroup);

app.UseCors(MyAllowSpecificOrigins);

app.ApplyMigrations();

app.UseMetricServer();

app.UseHttpMetrics();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

    foreach (ApiVersionDescription description in descriptions)
    {
        string url = $"/swagger/{description.GroupName}/swagger.json";
        string name = description.GroupName.ToUpperInvariant();

        options.SwaggerEndpoint(url, name);
    }
});

app.UseExceptionHandler();

app.Run();

public partial class Program;