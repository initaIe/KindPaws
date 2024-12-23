using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.SharedKernel.DDD;

public abstract class Entity<TId> : IEntity<TId> where TId : IEquatable<TId>
{
    protected Entity(
        TId id,
        CreatedAt createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public TId Id { get; init; }
    public CreatedAt CreatedAt { get; init; }
    public LastModifiedAt? LastModifiedAt { get; private set; }

    protected void UpdateLastModifiedAt()
        => LastModifiedAt = LastModifiedAt.CreateNew();
}