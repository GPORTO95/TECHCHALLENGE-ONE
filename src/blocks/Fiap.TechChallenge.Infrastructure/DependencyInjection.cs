using Fiap.TechChallenge.Application.Abstractions.Data;
using Fiap.TechChallenge.Application.Abstractions.EventBus;
using Fiap.TechChallenge.Infrastructure.Data;
using Fiap.TechChallenge.Infrastructure.MessageBroker;
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

        AddEventBus(services);

        return services;
    }

    private static void AddRepositories(IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        services.AddDbContext<ApplicationDbContext>(
            (sp, options) => options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
    }

    private static void AddEventBus(IServiceCollection services)
    {
        services.AddTransient<IEventBus, EventBus>();
    }
}
