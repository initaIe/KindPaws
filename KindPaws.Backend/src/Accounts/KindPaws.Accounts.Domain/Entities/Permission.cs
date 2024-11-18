using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Domain.Entities;

public class Permission : Entity<PermissionId>
{
    // ef core
    private Permission(PermissionId id)
        : base(id)
    {
    }

    public Permission(
        PermissionId id,
        PermissionCode code)
        : base(id)
    {
        Code = code;
    }

    public PermissionCode Code { get; private set; }
}