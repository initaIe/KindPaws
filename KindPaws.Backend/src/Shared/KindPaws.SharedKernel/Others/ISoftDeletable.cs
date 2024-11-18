using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.SharedKernel.Others;

public interface ISoftDeletable
{
    bool IsSoftDeleted { get; }
    UtcNowTimestamp? SoftDeletionTimestamp { get; }
}