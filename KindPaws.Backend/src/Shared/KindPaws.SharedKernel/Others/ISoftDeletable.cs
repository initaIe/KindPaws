namespace KindPaws.SharedKernel.Others;

public interface ISoftDeletable
{
    bool IsSoftDeleted { get; }
    DateTime? SoftDeletionTimestamp { get; }
}