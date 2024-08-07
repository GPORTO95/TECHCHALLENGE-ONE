using FluentValidation;

namespace Fiap.TechChallenge.Listagem.API;

public static class DependencyInjection
{
    public static IServiceCollection AddContatoApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        return services;
    }
}
