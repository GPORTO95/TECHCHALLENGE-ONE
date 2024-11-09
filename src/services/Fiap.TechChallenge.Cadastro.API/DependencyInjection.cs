using Fiap.TechChallenge.Application.Abstractions.EventBus;
using Fiap.TechChallenge.Cadastro.API.Events;
using Fiap.TechChallenge.Infrastructure.MessageBroker;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Fiap.TechChallenge.Cadastro.API;

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

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumer<ContatoInseridoEventConsumer>();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

                // Check if the Host is valid
                if (string.IsNullOrEmpty(settings.Host))
                {
                    throw new Exception("RabbitMQ host cannot be null or empty.");
                }

                // Log the RabbitMQ connection details
                var rabbitMqUri = new Uri($"amqp://{settings.Username}:{settings.Password}@{settings.Host}");
                Console.WriteLine($"Connecting to RabbitMQ at: {rabbitMqUri}");

                configurator.Host(rabbitMqUri, h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

        services.AddTransient<IEventBus, EventBus>();

        return services;
    }
}