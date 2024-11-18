using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Domain.Entities;

public class RolePermission : Entity<RolePermissionId>
{
    // ef core
    private RolePermission(RolePermissionId id)
        : base(id)
    {
    }

    private RolePermission(
        RolePermissionId id,
        Guid roleId,
        PermissionId permissionId)
        : base(id)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }
    public PermissionId PermissionId { get; private set; }
    public Permission Permission { get; private set; }

    public static Result<RolePermission, Error> Create(
        RolePermissionId id,
        Guid roleId,
        PermissionId permissionId)
    {
        if (GuidValidator.IsEmpty(id))
            return Errors.General.ValueIsInvalid(nameof(RoleId));

        return new RolePermission(
            id,
            roleId,
            permissionId);
    }
}