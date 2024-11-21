using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Domain.Entities;

public class AccountRole
{
    // ef core
    private AccountRole(AccountRoleId id)
    {
        Id = id;
    }

    private AccountRole(
        AccountRoleId id,
        RoleId roleId,
        DateTime creationTimestamp)
    {
        Id = id;
        RoleId = roleId;
        CreationTimestamp = creationTimestamp;
    }

    public AccountRoleId Id { get; private set; }
    public RoleId RoleId { get; private set; }
    public DateTime CreationTimestamp { get; private set; }

    public static AccountRole CreateNew(RoleId roleId)
    {
        var id = AccountRoleId.CreateRandom();
        var creationTimestamp = DateTime.UtcNow;
        return new AccountRole(id, roleId, creationTimestamp);
    }

    public static Result<AccountRole, Error> Create(
        AccountRoleId accountRoleId,
        RoleId roleId,
        DateTime creationTimestamp)
    {
        if (creationTimestamp > DateTime.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(creationTimestamp));

        return new AccountRole(accountRoleId, roleId, creationTimestamp);
    }
}