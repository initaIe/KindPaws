namespace KindPaws.SharedKernel.Others;

public interface IEntity<out TId>
    where TId : IEquatable<TId>
{
    public TId Id { get; }
}