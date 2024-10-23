using Fiap.TechChallenge.Application.Abstractions.EventBus;
using Fiap.TechChallenge.Atualizacao.API.Events;
using Fiap.TechChallenge.Infrastructure.MessageBroker;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Fiap.TechChallenge.Atualizacao.API;

public static class DependencyInjection
{
    public static IServiceCollection AddContatoApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        return services;
    }

    public static IServiceCollection AddContatoInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageBrokerSettings>(configuration.GetSection("MessageBroker"));

        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);



        var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
        var rabbitMqUser = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest";
        var rabbitMqPass = Environment.GetEnvironmentVariable("RABBITMQ_PASS") ?? "guest";

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumer<ContatoAtualizadoEventConsumer>();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

                if(settings.Host == string.Empty || settings.Username == string.Empty || settings.Password == string.Empty)
                {
                    configurator.Host(new Uri(rabbitMqHost), h =>
                    {
                        h.Username(rabbitMqUser);
                        h.Password(rabbitMqPass);
                    });
                }
                else
                {
                   configurator.Host(new Uri(settings.Host), h =>
                   {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });
                }
              

                configurator.ConfigureEndpoints(context);
            });
        });

        services.AddTransient<IEventBus, EventBus>();

        return services;
    }
}
