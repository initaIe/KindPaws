using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Domain.Role;

public class RolePermission
{
    // ef core
    private RolePermission()
    {
    }

    public RolePermission(
        Guid roleId,
        PermissionId permissionId,
        DateTime creationTimestamp)
    {
        RoleId = roleId;
        PermissionId = permissionId;
        CreationTimestamp = creationTimestamp;
    }

    public Guid RoleId { get; private set; }
    public PermissionId PermissionId { get; private set; }
    public DateTime CreationTimestamp { get; private set; }
}