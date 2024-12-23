using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.SharedKernel.DDD;

public interface IEntity<TId> : IEntity where TId : IEquatable<TId>
{
    TId Id { get; }
}

public interface IEntity
{
    CreatedAt CreatedAt { get; }
    LastModifiedAt? LastModifiedAt { get; }
}