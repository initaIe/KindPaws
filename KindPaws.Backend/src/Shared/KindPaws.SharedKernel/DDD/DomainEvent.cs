namespace KindPaws.SharedKernel.DDD;

public abstract record DomainEvent : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTimeOffset EventOccurredAt { get; } = DateTimeOffset.UtcNow;
}