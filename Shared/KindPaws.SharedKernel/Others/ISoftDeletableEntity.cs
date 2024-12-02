namespace KindPaws.SharedKernel.Others;

public interface ISoftDeletableEntity<TId>
    : IEntity<TId>, ISoftDeletable
    where TId : IEquatable<TId>;