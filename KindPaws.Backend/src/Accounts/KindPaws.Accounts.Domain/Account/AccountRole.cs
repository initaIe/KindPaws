using Microsoft.AspNetCore.Identity;

namespace KindPaws.Accounts.Domain.Account;

public sealed class AccountRole : IdentityUserRole<Guid>
{
    // ef core
    private AccountRole()
    {
    }

    public AccountRole(
        Guid userId,
        Guid roleId,
        DateTime creationTimestamp)
    {
        UserId = userId;
        RoleId = roleId;
        CreationTimestamp = creationTimestamp;
    }

    public override Guid UserId { get; set; }
    public override Guid RoleId { get; set; }
    public DateTime CreationTimestamp { get; private set; }
}