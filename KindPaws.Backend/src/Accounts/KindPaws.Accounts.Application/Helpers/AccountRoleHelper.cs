using KindPaws.Accounts.Domain.Entities;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Application.Helpers;

public static class AccountRoleHelper
{
    public static AccountRole ForceCreateNewAccountRole(Guid roleId)
    {
        var accountRoleRoleId = RoleId.Create(roleId).Value;
        return AccountRole.CreateNew(accountRoleRoleId);
    }
}