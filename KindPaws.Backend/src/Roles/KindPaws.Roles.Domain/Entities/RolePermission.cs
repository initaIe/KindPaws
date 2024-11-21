using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Roles.Domain.Entities;

public class RolePermission
{
    // ef core
    private RolePermission()
    {
    }

    private RolePermission(
        RolePermissionId id,
        PermissionId permissionId,
        DateTime creationTimestamp)
    {
        Id = id;
        PermissionId = permissionId;
        CreationTimestamp = creationTimestamp;
    }

    public RolePermissionId Id { get; private set; }
    public PermissionId PermissionId { get; private set; }
    public DateTime CreationTimestamp { get; private set; }

    public static RolePermission CreateNew(PermissionId permissionId)
    {
        var id = RolePermissionId.CreateRandom();
        var creationTimestamp = DateTime.UtcNow;
        return new RolePermission(id, permissionId, creationTimestamp);
    }

    public static Result<RolePermission, Error> Create(
        RolePermissionId id,
        PermissionId permissionId,
        DateTime creationTimestamp)
    {
        if (creationTimestamp > DateTime.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(creationTimestamp));

        return new RolePermission(id, permissionId, creationTimestamp);
    }
}