namespace KindPaws.SharedKernel.Others;

public interface ISoftDeletable
{
    bool IsSoftDeleted { get; }
    DateTimeOffset? SoftDeletionTimestamp { get; }
}