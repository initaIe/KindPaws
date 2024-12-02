namespace KindPaws.SharedKernel.Others;

public interface ISoftDeletableAggregateRoot<TId> 
    : IAggregateRoot<TId>, ISoftDeletable
    where TId : IEquatable<TId>;