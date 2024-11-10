using Fiap.TechChallenge.Application.Abstractions.Data;
using Fiap.TechChallenge.Infrastructure.Data;
using Fiap.TechChallenge.Kernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fiap.TechChallenge.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        AddRepositories(services, configuration, loggerFactory);

        return services;
    }

    private static void AddRepositories(IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        var logger  = loggerFactory.CreateLogger("Test");


        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);


        var sqlUser = Environment.GetEnvironmentVariable("MSSQL_USER");

        var saPassword = Environment.GetEnvironmentVariable("SA_PASSWORD");

        var host = Environment.GetEnvironmentVariable("MSSQL_HOST");

        connectionString = connectionString.Replace("{MSSQL_USER}",sqlUser)
                                           .Replace("{SA_PASSWORD}", saPassword)
                                           .Replace("{MSSQL_HOST}", host);

        logger.LogInformation("TestConnection");
        logger.LogInformation(connectionString);


        services.AddDbContext<ApplicationDbContext>(
            (sp, options) => options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
    }
}
    