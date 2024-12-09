using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Auth.Application.Factories;

public static class AccountFactory
{
    public static Account ForceCreateNew(
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