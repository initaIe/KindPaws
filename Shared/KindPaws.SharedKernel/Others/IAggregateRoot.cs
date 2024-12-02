namespace KindPaws.SharedKernel.Others;

public interface IAggregateRoot<TId>
    : IEntity<TId>
    where TId : IEquatable<TId>
{   
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
}