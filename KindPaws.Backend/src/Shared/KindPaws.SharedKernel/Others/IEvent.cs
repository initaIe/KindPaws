namespace KindPaws.SharedKernel.Others;

public interface IEvent
{
    Guid EventId => Guid.NewGuid();
    public DateTimeOffset OccurredAt => DateTimeOffset.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName!;
}