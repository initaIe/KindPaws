using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Domain.UsersManagement.AggregateRoot;

namespace KindPaws.Users.Application.Factories;

public static class UserFactory
{
    public static User ForceCreateNew(
        string username,
        string emailAddress,
        Guid accountId)
    {
        var userName = UserName.Create(username).Value;
        var userEmailAddress = EmailAddress.Create(emailAddress).Value;
        var userAccountId = AccountId.Create(accountId).Value;

        return User.CreateNew(userName, userEmailAddress, userAccountId);
    }
}