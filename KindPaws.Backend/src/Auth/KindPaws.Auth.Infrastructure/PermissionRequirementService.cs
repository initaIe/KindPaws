using KindPaws.Accounts.Contracts;
using KindPaws.Permissions.Contracts;
using KindPaws.Roles.Contracts;

namespace KindPaws.Auth.Infrastructure;

public class PermissionRequirementService
{
    private readonly IAccountsContract _accountsContract;
    private readonly IRolesContract _rolesContract;
    private readonly IPermissionsContract _permissionsContract;

    public PermissionRequirementService(
        IAccountsContract accountsContract,
        IRolesContract rolesContract,
        IPermissionsContract permissionsContract)
    {
        _accountsContract = accountsContract;
        _rolesContract = rolesContract;
        _permissionsContract = permissionsContract;
    }

    public async Task<bool> HasRequiredPermission(Guid accountId, string permissionCode)
    {
        var accountRoles = await _accountsContract.GetAccountRolesByIdAsync(accountId);

        if (accountRoles.Count == 0)
            return false;

        var roleIds = accountRoles.Select(ar => ar.RoleId);
        var rolePermissions = await _rolesContract.GetRolePermissionsByIdsAsync(roleIds);

        if (rolePermissions.Count == 0)
            return false;

        var requiredPermissionId = await _permissionsContract.GetPermissionIdByCodeAsync(permissionCode);

        if (requiredPermissionId.IsFailure)
            throw new ApplicationException("Required permission is not found.");

        var permissionIds = rolePermissions.Select(rp => rp.PermissionId);

        return permissionIds.Contains(requiredPermissionId.Value);
    }
}