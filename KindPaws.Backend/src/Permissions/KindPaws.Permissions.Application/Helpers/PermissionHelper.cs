using KindPaws.Permissions.Domain.AggregateRoot;
using KindPaws.Permissions.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Permissions.Application.Helpers;

public static class PermissionHelper
{
    public static Permission ForceCreateNewPermission(string code)
    {
        var permissionCode = PermissionCode.Create(code).Value;
        return Permission.CreateNew(permissionCode);
    }
}