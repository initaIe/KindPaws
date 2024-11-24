using KindPaws.Roles.Domain.Entities;
using KindPaws.Roles.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Roles.Domain.AggregateRoot;

public sealed class Role : IEntity<RoleId>
{
    private readonly List<RolePermission> _rolePermissions = [];

    // ef core
    public Role()
    {
    }

    private Role(
        RoleId id,
        RoleName name,
        DateTime creationTimestamp)
    {
        Id = id;
        Name = name;
        CreationTimestamp = creationTimestamp;
    }

    public RoleId Id { get; private set; }
    public RoleName Name { get; private set; }
    public DateTime CreationTimestamp { get; private set; }
    public IReadOnlyList<RolePermission> RolePermissions => _rolePermissions;

    public static Role CreateNew(RoleName name)
    {
        var id = RoleId.CreateRandom();
        var creationTimestamp = DateTime.UtcNow;
        return new Role(id, name, creationTimestamp);
    }

    public static Result<Role, Error> Create(
        RoleId id,
        RoleName name,
        DateTime creationTimestamp)
    {
        if (creationTimestamp > DateTime.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(creationTimestamp));

        return new Role(id, name, creationTimestamp);
    }

    public Result<RolePermission, Error> GetRolePermissionById(RolePermissionId rolePermissionId)
    {
        var rolePermission = _rolePermissions.FirstOrDefault(p => p.Id == rolePermissionId);

        if (rolePermission == null)
            return Errors.General.RecordNotFound(
                nameof(RolePermission),
                nameof(RolePermissionId),
                rolePermissionId.Value);

        return rolePermission;
    }

    public Result<Error> DeleteRolePermission(RolePermissionId rolePermissionId)
    {
        var rolePermission = GetRolePermissionById(rolePermissionId);

        if (rolePermission.IsFailure)
            return rolePermission.Error;

        _rolePermissions.Remove(rolePermission.Value);
        return true;
    }

    public void AddRolePermission(RolePermission rolePermission)
    {
        _rolePermissions.Add(rolePermission);
    }

    public void AddRolePermissions(IEnumerable<RolePermission> rolePermissions)
    {
        _rolePermissions.AddRange(rolePermissions);
    }
}