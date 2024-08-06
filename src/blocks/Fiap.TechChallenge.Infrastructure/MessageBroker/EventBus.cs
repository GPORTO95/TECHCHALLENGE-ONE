using Fiap.TechChallenge.Application.Abstractions.EventBus;
using MassTransit;

namespace Fiap.TechChallenge.Infrastructure.MessageBroker;

public sealed class EventBus(IPublishEndpoint publish) : IEventBus
{
    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) 
        where T : class => publish.Publish(message, cancellationToken);
}
