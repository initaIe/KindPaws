using System.Runtime.InteropServices.JavaScript;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Domain;

public sealed class AccountRole
{
    // ef core
    private AccountRole()
    {
    }

    private AccountRole(
        AccountId accountId,
        RoleId roleId, 
        DateTime creationTimestamp)
    {
        AccountId = accountId;
        RoleId = roleId;
        CreationTimestamp = creationTimestamp;
    }

    public AccountId AccountId { get; private set; }
    public RoleId RoleId { get; private set; }
    public DateTime CreationTimestamp { get; private set; }

    public static AccountRole CreateNew(AccountId accountId, RoleId roleId)
    {
        var creationTimestamp = DateTime.UtcNow;
        return new AccountRole(accountId, roleId, creationTimestamp);
    }
    
    public static Result<AccountRole, Error> Create(AccountId accountId, RoleId roleId, DateTime creationTimestamp)
    {
        if (creationTimestamp > DateTime.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(creationTimestamp));
        
        return new AccountRole(accountId, roleId, creationTimestamp);
    }
}