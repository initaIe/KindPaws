using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.SharedKernel.Others;

public abstract class AggregateRoot<TId>
    : Entity<TId>, IAggregateRoot<TId>
    where TId : IEquatable<TId>
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected AggregateRoot(
        TId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}