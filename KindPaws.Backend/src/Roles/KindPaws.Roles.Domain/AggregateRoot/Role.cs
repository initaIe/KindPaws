using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Roles.Domain.AggregateRoot;

public sealed class Role : Entity<UserRoleId>
{
    private List<PermissionId> _permissions = [];

    // ef core
    private Role()
    {
    }

    public Role(
        UserRoleId id,
        RoleName name,
        CreatedAt createdAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
    }

    public UserRoleId Id { get; private set; }
    public RoleName Name { get; private set; }
    public CreatedAt CreatedAt { get; private set; }
    public IReadOnlyList<PermissionId> Permissions => _permissions;

    #region Factory methods

    public static Role CreateNew(RoleName name)
    {
        var id = UserRoleId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();
        return new Role(id, name, createdAt);
    }

    #endregion

    #region CRUD

    public void DeletePermission(PermissionId permissionId)
    {
        _permissions.Remove(permissionId);
    }

    public void DeletePermissions(IEnumerable<PermissionId> permissionIds)
    {
        foreach (var permissionId in permissionIds)
        {
            _permissions.Remove(permissionId);
        }
    }

    public void AddPermission(PermissionId permissionId)
    {
        _permissions.Add(permissionId);
    }

    public void AddPermissions(IEnumerable<PermissionId> permissionIds)
    {
        _permissions.AddRange(permissionIds);
    }

    #endregion
}