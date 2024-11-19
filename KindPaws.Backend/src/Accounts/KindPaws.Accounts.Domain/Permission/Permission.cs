using KindPaws.Accounts.Domain.Permission.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Domain.Permission;

public class Permission : IEntity<PermissionId>
{
    // ef core
    private Permission()
    {
    }

    public Permission(
        PermissionId id,
        PermissionCode code)
    {
        Id = id;
        Code = code;
    }

    public PermissionId Id { get; }
    public PermissionCode Code { get; private set; }
}