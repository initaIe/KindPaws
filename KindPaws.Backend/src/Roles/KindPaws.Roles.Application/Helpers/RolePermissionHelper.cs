using KindPaws.Roles.Domain.Entities;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Roles.Application.Helpers;

public static class RolePermissionHelper
{
    public static RolePermission ForceCreateNewRolePermission(Guid permissionId)
    {
        var rolePermissionPermissionId = PermissionId.Create(permissionId).Value;
        return RolePermission.CreateNew(rolePermissionPermissionId);
    }
}