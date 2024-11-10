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
                var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
                var rabbitMqUser = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest";
                var rabbitMqPass = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest";

                Console.WriteLine($"Connecting to RabbitMQ at: {rabbitMqHost},{rabbitMqUser},{rabbitMqPass}");

                    configurator.Host(new Uri($"amqp://{rabbitMqHost}"), h =>
                    {
                        h.Username(rabbitMqUser);
                        h.Password(rabbitMqPass);
                    });

                    configurator.ConfigureEndpoints(context);
                });
        });

        services.AddTransient<IEventBus, EventBus>();

        return services;
    }
}