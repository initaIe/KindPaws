namespace KindPaws.Domain.Shared.Others;

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