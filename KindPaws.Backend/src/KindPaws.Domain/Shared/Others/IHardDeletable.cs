namespace KindPaws.Domain.Shared.Others;

public interface IHardDeletable : IHardDeleteState, IHardDeleteAction;

public interface IHardDeleteState
{
    bool IsHardDeleted { get; }
}

public interface IHardDeleteAction
{
    void HardDelete();
}