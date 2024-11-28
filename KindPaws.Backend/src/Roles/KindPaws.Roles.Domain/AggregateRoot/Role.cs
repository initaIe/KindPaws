using KindPaws.Roles.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Roles.Domain.AggregateRoot;

public sealed class Role : IEntity<RoleId>
{
    private List<PermissionId> _permissions = [];

    // ef core
    private Role()
    {
    }

    public Role(
        RoleId id,
        RoleName name,
        CreationTimestamp creationTimestamp)
    {
        Id = id;
        Name = name;
        CreationTimestamp = creationTimestamp;
    }

    public RoleId Id { get; private set; }
    public RoleName Name { get; private set; }
    public CreationTimestamp CreationTimestamp { get; private set; }
    public IReadOnlyList<PermissionId> Permissions => _permissions;

    #region Factory methods

    public static Role CreateNew(RoleName name)
    {
        var id = RoleId.CreateRandom();
        var creationTimestamp = CreationTimestamp.CreateNew();
        return new Role(id, name, creationTimestamp);
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