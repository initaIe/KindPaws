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
        var getRolesResult = await _accountsContract.GetRolesAsync(accountId);

        if (getRolesResult.IsFailure)
            return false;

        if (getRolesResult.Value.Count == 0)
            return false;

        var getPermissions = await _rolesContract.GetPermissionsByRoleIdsAsync(getRolesResult.Value);

        if (getPermissions.Count == 0)
            return false;

        var requiredPermissionId = await _permissionsContract.GetPermissionIdByCodeAsync(permissionCode);

        if (requiredPermissionId.IsFailure)
            throw new ApplicationException("Required permission is not found.");

        return getPermissions.Contains(requiredPermissionId.Value);
    }
}