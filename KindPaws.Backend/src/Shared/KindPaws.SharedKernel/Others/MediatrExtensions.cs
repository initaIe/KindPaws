using MediatR;

namespace KindPaws.SharedKernel.Others;

public static class MediatrExtensions
{
    public static async Task PublishDomainEvents<TId>(
        this IPublisher publisher,
        AggregateRoot<TId> entity,
        CancellationToken cancellationToken = default)
        where TId : IEquatable<TId>
    {
        foreach (var domainEvent in entity.DomainEvents)
        {
            await publisher.Publish(domainEvent, cancellationToken);
        }

        entity.ClearDomainEvents();
    }
}