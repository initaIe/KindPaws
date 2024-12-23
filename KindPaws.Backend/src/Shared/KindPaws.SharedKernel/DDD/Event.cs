namespace KindPaws.SharedKernel.DDD;

public abstract record Event : IEvent
{
    public Guid Id => Guid.NewGuid();
    public DateTimeOffset OccurredAt => DateTimeOffset.UtcNow;
}