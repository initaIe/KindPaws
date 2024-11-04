namespace KindPaws.SharedKernel.Others.DeletionManagement;

public interface ISoftDeletable : ISoftDeleteState, ISoftDeleteAction;

public interface ISoftDeleteState
{
    bool IsSoftDeleted { get; }
    DateTime? SoftDeletedDateTime { get; }
}

public interface ISoftDeleteAction
{
    void SoftDelete();
    void Restore();
}