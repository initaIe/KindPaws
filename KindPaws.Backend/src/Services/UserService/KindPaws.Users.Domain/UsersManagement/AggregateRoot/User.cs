using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Domain.UsersManagement.Entities;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Users.Domain.UsersManagement.AggregateRoot;

// TODO: ADD SOFT DELETE?
// TODO: MOVE PHONENUMBER AND EMAIL TO VO Contacts
public sealed class User : AggregateRoot<UserId>
{
    private List<UserRoleId> _roles = [];

    #region EF Core constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private User(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        UserId id,
        CreatedAt createdAt, AccountId accountId, PhoneNumber phoneNumber)
        : base(id, createdAt)
    {
        AccountId = accountId;
        PhoneNumber = phoneNumber;
    }

    #endregion

    private User(
        UserId id,
        CreatedAt createdAt,
        Username username,
        EmailAddress emailAddress,
        AccountId accountId)
        : base(id, createdAt)
    {
        Username = username;
        EmailAddress = emailAddress;
        AccountId = accountId;
    }

    public Username Username { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public Profile Profile { get; private set; } = Profile.CreateNew();
    public UserReputation Reputation { get; private set; } = UserReputation.Default;
    public AccountId AccountId { get; }
    public IReadOnlyList<UserRoleId> Roles => _roles;

    #region Factory methods

    public static User CreateNew(
        Username username,
        EmailAddress emailAddress,
        AccountId accountId)
    {
        var id = UserId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new User(
            id,
            createdAt,
            username,
            emailAddress,
            accountId);
    }

    public static User Create(
        UserId id,
        CreatedAt createdAt,
        Username username,
        EmailAddress emailAddress,
        PhoneNumber phoneNumber,
        AccountId accountId)
    {
        return new User(
            id,
            createdAt,
            username,
            emailAddress,
            accountId);
    }

    #endregion

    #region CRUD

    public bool IsUserHasRole(UserRoleId userRoleId)
        => _roles.Contains(userRoleId);

    // TODO: ADD LAST MOFIED
    public void AddRole(UserRoleId userRoleId)
    {
        var isUserHasRole = IsUserHasRole(userRoleId);

        if (isUserHasRole)
            return;

        _roles.Add(userRoleId);
    }

    public void RemoveRole(UserRoleId userRoleId)
        => _roles.Remove(userRoleId);

    public void UpdateEmailAddress(EmailAddress emailAddress)
        => EmailAddress = emailAddress;

    #endregion
}