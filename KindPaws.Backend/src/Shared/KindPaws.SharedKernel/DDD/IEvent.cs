using MediatR;

namespace KindPaws.SharedKernel.DDD;

public interface IEvent : INotification
{
    public Guid EventId { get; }
    public DateTimeOffset EventOccurredAt { get; }
    public string EventType => GetType().AssemblyQualifiedName!;
}