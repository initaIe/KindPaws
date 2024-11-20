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
        RoleId roleId,
        PermissionId permissionId,
        DateTime creationTimestamp)
    {
        RoleId = roleId;
        PermissionId = permissionId;
        CreationTimestamp = creationTimestamp;
    }

    public RoleId RoleId { get; private set; }
    public PermissionId PermissionId { get; private set; }
    public DateTime CreationTimestamp { get; private set; }

    public static RolePermission CreateNew(
        RoleId roleId,
        PermissionId permissionId)
    {
        var creationTimestamp = DateTime.UtcNow;
        return new RolePermission(roleId, permissionId, creationTimestamp);
    }
    
    public static Result<RolePermission, Error> Create(
        RoleId roleId,
        PermissionId permissionId, 
        DateTime creationTimestamp)
    {
        if (creationTimestamp > DateTime.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(creationTimestamp));
        
        return new RolePermission(roleId, permissionId, creationTimestamp);
    }
}