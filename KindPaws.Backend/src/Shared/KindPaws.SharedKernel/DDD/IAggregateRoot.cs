namespace KindPaws.SharedKernel.DDD;

public interface IAggregateRoot<TId> : IAggregateRoot, IEntity<TId> where TId : IEquatable<TId>;

public interface IAggregateRoot
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    // void AddDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}