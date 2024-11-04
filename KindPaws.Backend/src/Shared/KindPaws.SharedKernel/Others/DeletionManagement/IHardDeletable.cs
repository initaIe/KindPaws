namespace KindPaws.SharedKernel.Others.DeletionManagement;

public interface IHardDeletable : IHardDeleteState, IHardDeleteAction;

public interface IHardDeleteState
{
    bool IsHardDeleted { get; }
}

public interface IHardDeleteAction
{
    void HardDelete();
}