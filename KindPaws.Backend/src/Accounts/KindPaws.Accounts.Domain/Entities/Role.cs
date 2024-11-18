using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using Microsoft.AspNetCore.Identity;

namespace KindPaws.Accounts.Domain.Entities;

public sealed class Role : IdentityRole<Guid>
{
    private readonly List<RolePermission> _rolePermissions = [];

    public Role(
        Guid id,
        ShortAlphabeticString name)
    {
        Id = id;
        Name = name.Value;
    }

    public override Guid Id { get; set; }
    public override string? Name { get; set; }
    public IReadOnlyList<RolePermission> RolePermissions => _rolePermissions;

    public static Result<Role, Error> Create(
        Guid id,
        ShortAlphabeticString name)
    {
        if (GuidValidator.IsEmpty(id))
            return Errors.General.ValueIsInvalid("RoleId");

        return new Role(id, name);
    }
}