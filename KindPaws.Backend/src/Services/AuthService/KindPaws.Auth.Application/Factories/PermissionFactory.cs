using KindPaws.Auth.Domain.PermissionsManagement.AggregateRoot;
using KindPaws.Auth.Domain.PermissionsManagement.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Auth.Application.Factories;

public static class PermissionFactory
{
    public static Permission ForceCreateNew(string code)
    {
        var permissionCode = PermissionCode.Create(code).Value;

        return Permission.CreateNew(permissionCode);
    }
}