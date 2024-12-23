using KindPaws.SharedKernel.DDD;
using MediatR;

namespace KindPaws.SharedKernel.Others;

public static class MediatrExtensions
{
    public static async Task PublishDomainEventsAsync(
        this IPublisher publisher,
        IAggregateRoot aggregateRoot,
        CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in aggregateRoot.DomainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }

        aggregateRoot.ClearDomainEvents();
    }

    public static async Task PublishEventsAsync(
        this IPublisher publisher,
        IEnumerable<IEvent> events,
        CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in events)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }
    }
}