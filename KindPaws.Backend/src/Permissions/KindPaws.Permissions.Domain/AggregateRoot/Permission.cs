using KindPaws.Permissions.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Permissions.Domain.AggregateRoot;

public class Permission : IEntity<PermissionId>
{
    // ef core
    private Permission()
    {
    }

    private Permission(
        PermissionId id,
        PermissionCode code,
        DateTime creationTimestamp)
    {
        Id = id;
        Code = code;
        CreationTimestamp = creationTimestamp;
    }

    public PermissionId Id { get; private set; }
    public PermissionCode Code { get; private set; }
    public DateTime CreationTimestamp { get; private set; }

    public static Permission Create(
        PermissionId id,
        PermissionCode code,
        DateTime creationTimestamp)
    {
        // TODO: add validation
        return new Permission(id, code, creationTimestamp);
    }

    public static Permission CreateNew(PermissionCode code)
    {
        var id = PermissionId.CreateRandom();
        var creationTimestamp = DateTime.UtcNow;
        return new Permission(id, code, creationTimestamp);
    }
}