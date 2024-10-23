using Fiap.TechChallenge.Application.Abstractions.Data;
using Fiap.TechChallenge.Infrastructure.Data;
using Fiap.TechChallenge.Kernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.TechChallenge.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services, configuration);

        return services;
    }

    private static void AddRepositories(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database");

        if(connectionString == null) 
        {
            var sqlServerHost = Environment.GetEnvironmentVariable("SQLSERVER_HOST") ?? "sqlserver-service";
            var sqlServerUser = "sa";
            var sqlServerPassword = Environment.GetEnvironmentVariable("SQLSERVER_PASSWORD") ?? "YourStrong!Passw0rd"; 
            var databaseName = "PublicEnterpriseDb";

            connectionString = $"Server={sqlServerHost};Database={databaseName};User Id={sqlServerUser};Password={sqlServerPassword};";
        }



        services.AddDbContext<ApplicationDbContext>(
            (sp, options) => options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
    }
}
