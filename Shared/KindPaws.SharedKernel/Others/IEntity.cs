namespace KindPaws.SharedKernel.Others;

public interface IEntity<TId>
    where TId : IEquatable<TId>
{
    public TId Id { get; }
}