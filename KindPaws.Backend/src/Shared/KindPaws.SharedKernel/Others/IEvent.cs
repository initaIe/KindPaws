using MediatR;

namespace KindPaws.SharedKernel.Others;

public interface IEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTimeOffset OccurredAt => DateTimeOffset.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName!;
}