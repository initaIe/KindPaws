using KindPaws.Auth.Domain.PermissionsManagement.AggregateRoot;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Auth.Domain.RolesManagement.AggregateRoot;

public class Role : AggregateRoot<AccountRoleId>
{
    private List<PermissionId> _permissions = [];

    #region EF Core constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Role(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        AccountRoleId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    #endregion

    private Role(
        AccountRoleId id,
        CreatedAt createdAt,
        RoleName name)
        : base(id, createdAt)
    {
        Name = name;
    }

    public RoleName Name { get; private set; }
    public IReadOnlyList<PermissionId> Permissions => _permissions;

    #region Factory methods

    public static Role CreateNew(RoleName name)
    {
        var id = AccountRoleId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new Role(
            id,
            createdAt,
            name);
    }

    public static Role Create(
        AccountRoleId id,
        CreatedAt createdAt,
        RoleName name)
    {
        return new Role(
            id,
            createdAt,
            name);
    }

    #endregion

    #region Role CRUD

    public void UpdateName(RoleName name)
    {
        UpdateLastModifiedAt();
        Name = name;
    }

    #endregion

    #region Permissionds CRUD

    public bool HasPermission(PermissionId permissionId)
        => _permissions.Contains(permissionId);

    public Result<Error> AddPermission(PermissionId permissionId)
    {
        var isPermissionAlreadyExist = HasPermission(permissionId);

        if (isPermissionAlreadyExist)
            return GeneralErrors.RecordAlreadyExist(nameof(Permission), nameof(PermissionId));

        _permissions.Add(permissionId);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> AddPermissions(IEnumerable<PermissionId> permissionIds)
    {
        foreach (var permissionId in permissionIds)
        {
            var addPermissionResult = AddPermission(permissionId);

            if (addPermissionResult.IsFailure)
                return addPermissionResult.Error;
        }

        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> DeletePermission(PermissionId permissionId)
    {
        var isPermissionExist = HasPermission(permissionId);

        if (!isPermissionExist)
            return GeneralErrors.RecordNotFound(nameof(Permission), nameof(PermissionId));

        _permissions.Remove(permissionId);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> DeletePermissions(IEnumerable<PermissionId> permissionIds)
    {
        foreach (var permissionId in permissionIds)
        {
            var deletePermissionResult = DeletePermission(permissionId);

            if (deletePermissionResult.IsFailure)
                return deletePermissionResult.Error;
        }

        UpdateLastModifiedAt();
        return true;
    }

    #endregion
}