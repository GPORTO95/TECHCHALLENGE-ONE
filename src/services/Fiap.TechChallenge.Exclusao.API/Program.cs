using Fiap.TechChallenge.API.Infrastructure;
using Fiap.TechChallenge.Application;
using Fiap.TechChallenge.Exclusao.API;
using Fiap.TechChallenge.Exclusao.API.Repositories;
using Fiap.TechChallenge.Infrastructure;
using Fiap.TechChallenge.Infrastructure.Data;
using Fiap.TechChallenge.Kernel.Ddds;
using Microsoft.OpenApi.Models;
using Prometheus;
using System.Reflection;

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
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "API Exclusão Contato",
        Version = "v1",
        Description = "Esta API faz parte do desafio da pós graduação da FIAP",
        Contact = new OpenApiContact() { Name = "Departamento de Desenvolvimento", Email = "gr_porto@hotmail.com" },
    });
});

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services
    .AddContatoApplication()
    .AddContatoInfrastructure(builder.Configuration);

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IDddRepository, DddRepository>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection(); 
}

app.UseCors(MyAllowSpecificOrigins);

app.MapMetrics();

app.UseMetricServer();

app.UseHttpMetrics();

app.UseHttpMetrics();

app.MapControllers();

app.UseExceptionHandler();

app.Run();

public partial class Program;
