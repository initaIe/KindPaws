using KindPaws.Accounts.Domain.Account.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace KindPaws.Accounts.Domain.Account;

public sealed class Account : IdentityUser<Guid>, IEntity<Guid>
{
    private readonly List<Role.Role> _roles = [];
    private List<RefreshSession> _refreshSessions = [];
    private List<SocialNetwork> _socialNetworks = [];

    // ef Core
    private Account()
    {
    }

    private Account(
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
    public IReadOnlyList<RefreshSession> RefreshSessions => _refreshSessions;
    public IReadOnlyList<Role.Role> Roles => _roles;

    public static Result<Account, Error> Create(
        Guid id,
        UserName userName,
        EmailAddress email)
    {
        if (GuidValidator.IsEmpty(id))
            return Errors.General.ValueIsInvalid("UserId");

        return new Account(
            id,
            userName,
            email);
    }
    
    public Result<Error> AddRole(
        Role.Role role)
    {
        var hasRole = HasRole(role.Id);

        if (hasRole)
            return Errors.General.RecordAlreadyExist(nameof(Role.Role));

        _roles.Add(role);
        return true;
    }

    public bool HasRole(Guid roleId)
    {
        return _roles.Any(r=>r.Id == roleId);
    }

    public bool HasRefreshSessionByJti(Jti jti)
    {
        return _refreshSessions.Any(r=>r.Jti == jti);
    }
    
    public bool HasRefreshSession(RefreshSession refreshSession)
    {
        return _refreshSessions.Any(r=>r == refreshSession);
    }

    public Result<Error> AddRefreshSession(RefreshSession refreshSession)
    {
        var hasRefreshSession = HasRefreshSession(refreshSession);

        if (hasRefreshSession)
            return Errors.General.RecordAlreadyExist(nameof(RefreshSessions));
        
        _refreshSessions.Add(refreshSession);
        return true;
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