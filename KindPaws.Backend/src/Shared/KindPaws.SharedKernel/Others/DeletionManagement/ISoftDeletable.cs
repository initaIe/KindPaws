namespace KindPaws.SharedKernel.Others.DeletionManagement;

public interface ISoftDeletable
{
    bool IsSoftDeleted { get; }
    DateTime? SoftDeletedDateTime { get; }
}