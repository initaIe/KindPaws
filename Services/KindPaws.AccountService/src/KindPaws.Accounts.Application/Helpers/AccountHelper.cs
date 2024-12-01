using KindPaws.Accounts.Domain.AggregateRoot;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Accounts.Application.Helpers;

public static class AccountHelper
{
    public static Account ForceCreateNewAccount(
        string userName,
        string emailAddress,
        string passwordHash)
    {
        var accountUserName = UserName.Create(userName).Value;
        var accountEmailAddress = EmailAddress.Create(emailAddress).Value;
        var accountPasswordHash = PasswordHash.Create(passwordHash).Value;

        return Account.CreateNew(accountUserName, accountEmailAddress, accountPasswordHash);
    }
}