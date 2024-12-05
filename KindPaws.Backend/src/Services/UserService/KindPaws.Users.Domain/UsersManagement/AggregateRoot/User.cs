using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Domain.UsersManagement.Entities;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Users.Domain.UsersManagement.AggregateRoot;

// TODO: ADD SOFT DELETE?
public sealed class User : AggregateRoot<UserId>
{
    private List<RoleId> _roles = [];

    // ef core
    private User(
        UserId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    private User(
        UserId id,
        CreatedAt createdAt,
        UserName userName,
        EmailAddress emailAddress)
        : base(id, createdAt)
    {
        UserName = userName;
        EmailAddress = emailAddress;
    }

    public UserName UserName { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public Profile Profile { get; private set; } = Profile.CreateNew();
    public IReadOnlyList<RoleId> Roles => _roles;

    #region Factory methods

    public static User CreateNew(
        UserName userName,
        EmailAddress emailAddress)
    {
        var id = UserId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new User(
            id,
            createdAt,
            userName,
            emailAddress);
    }

    public static User Create(
        UserId id,
        CreatedAt createdAt,
        UserName userName,
        EmailAddress emailAddress)
    {
        return new User(
            id,
            createdAt,
            userName,
            emailAddress);
    }

    #endregion

    #region CRUD

    public bool IsUserHasRole(RoleId roleId)
        => _roles.Contains(roleId);

    // TODO: ADD LAST MOFIED
    public void AddRole(RoleId roleId)
    {
        var isUserHasRole = IsUserHasRole(roleId);

        if (isUserHasRole)
            return;

        _roles.Add(roleId);
    }

    public void RemoveRole(RoleId roleId)
        => _roles.Remove(roleId);

    public void UpdateEmailAddress(EmailAddress emailAddress)
        => EmailAddress = emailAddress;

    #endregion
}