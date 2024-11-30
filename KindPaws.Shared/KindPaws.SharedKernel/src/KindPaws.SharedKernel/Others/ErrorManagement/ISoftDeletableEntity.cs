namespace KindPaws.SharedKernel.Others.ErrorManagement;

public interface ISoftDeletableEntity<TId> : IEntity<TId>, ISoftDeletable
    where TId : IEquatable<TId>;