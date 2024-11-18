namespace KindPaws.SharedKernel.Others;

public abstract class Entity<TId>
    where TId : IEquatable<TId>
{
    protected Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; }
}