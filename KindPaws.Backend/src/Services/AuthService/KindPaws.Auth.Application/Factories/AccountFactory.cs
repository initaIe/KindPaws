using KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Auth.Application.Factories;

public static class AccountFactory
{
    public static Account ForceCreateNew(
        string userName,
        string emailAddress)
    {
        var accountUserName = UserName.Create(userName).Value;
        var accountEmailAddress = EmailAddress.Create(emailAddress).Value;

        return Account.CreateNew(accountUserName, accountEmailAddress);
    }
}