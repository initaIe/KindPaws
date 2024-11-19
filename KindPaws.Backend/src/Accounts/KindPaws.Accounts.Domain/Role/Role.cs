using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using Microsoft.AspNetCore.Identity;

namespace KindPaws.Accounts.Domain.Role;

public sealed class Role : IdentityRole<Guid>, IEntity<Guid>
{
    private readonly List<Permission.Permission> _permissions = [];

    public Role(
        Guid id,
        ShortAlphabeticString name)
    {
        Id = id;
        Name = name.Value;
    }

    public override Guid Id { get; set; }
    public override string? Name { get; set; }
    public IReadOnlyList<Permission.Permission> Permissions => _permissions;

    public static Result<Role, Error> Create(
        Guid id,
        ShortAlphabeticString name)
    {
        if (GuidValidator.IsEmpty(id))
            return Errors.General.ValueIsInvalid("RoleId");

        return new Role(id, name);
    }

    public bool HasPermission(PermissionId permissionId)
    {
       return _permissions.Any(p=>p.Id == permissionId);
    }
    
    public Result<Error> AddPermission(Permission.Permission permission)
    {
        var hasPermission = HasPermission(permission.Id);

        if (hasPermission)
            return Errors.General.RecordAlreadyExist(nameof(Permission.Permission), nameof(PermissionId));
        
        _permissions.Add(permission);
        return true;
    }
}