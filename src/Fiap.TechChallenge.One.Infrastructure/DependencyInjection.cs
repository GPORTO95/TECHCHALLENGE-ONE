using Fiap.TechChallenge.One.Application.Abstractions.Data;
using Fiap.TechChallenge.One.Domain.Kernel;
using Fiap.TechChallenge.One.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Fiap.TechChallenge.One.Domain.Contatos;
using Fiap.TechChallenge.One.Infrastructure.Repositories;
using Fiap.TechChallenge.One.Domain.Ddds;

namespace Fiap.TechChallenge.One.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        services.AddDbContext<ApplicationDbContext>(
            (sp, options) => options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IContatoRepository, ContatoRepository>();
        services.AddScoped<IDddRepository, DddRepository>();
    }
}
