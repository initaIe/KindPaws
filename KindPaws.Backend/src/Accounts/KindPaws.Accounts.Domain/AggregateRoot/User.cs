using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace KindPaws.Accounts.Domain.Entities;

public sealed class User : IdentityUser<Guid>
{
    private readonly List<Role> _roles = [];
    private readonly List<Permission> _permissions = [];
    private List<SocialNetwork> _socialNetworks = [];

    // ef Core
    private User()
    {
    }

    private User(
        Guid id,
        UserName userName,
        EmailAddress email)
    {
        Id = id;
        UserName = userName.Value;
        Email = email.Value;
    }

    public override Guid Id { get; set; }
    public override string? UserName { get; set; }
    public override string? Email { get; set; }
    public override string? PhoneNumber { get; set; }
    public FullName? FullName { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<Role> Roles => _roles;
    public IReadOnlyList<Permission> Permissions => _permissions;

    public static Result<User, Error> Create(
        Guid id,
        UserName userName,
        EmailAddress email)
    {
        if (GuidValidator.IsEmpty(id))
            return Errors.General.ValueIsInvalid("UserId");

        return new User(
            id,
            userName,
            email);
    }

    public void UpdateEmailAddress(EmailAddress emailAddress)
    {
        Email = emailAddress.Value;
    }

    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber.Value;
    }
}