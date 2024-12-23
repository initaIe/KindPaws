using MediatR;

namespace KindPaws.SharedKernel.DDD;

public interface IEvent : INotification
{
    public Guid Id { get; }
    public DateTimeOffset OccurredAt { get; }
    public string EventType => GetType().AssemblyQualifiedName!;
}