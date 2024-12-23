using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Auth.Application.Factories;

public static class AccountFactory
{
    public static Account ForceCreateNew(
        string userName,
        string emailAddress,
        string passwordHash,
        AccountRoleId defaultAccountRole)
    {
        var accountUsername = Username.Create(userName).Value;
        var accountEmailAddress = EmailAddress.Create(emailAddress).Value;
        var accountPasswordHash = PasswordHash.Create(passwordHash).Value;

        return Account.CreateNew(
            accountUsername,
            accountEmailAddress,
            accountPasswordHash,
            defaultAccountRole);
    }
}