using KindPaws.Accounts.Domain.Entities;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Domain.AggregateRoot;

public sealed class Account : IEntity<AccountId>
{
    private readonly List<RefreshSession> _refreshSessions = [];
    private readonly List<AccountRole> _accountRoles = [];
    private List<SocialNetwork> _socialNetworks = [];

    // ef Core
    private Account()
    {
    }

    private Account(
        AccountId id,
        UserName userName,
        EmailAddress emailAddress,
        PasswordHash passwordHash,
        DateTime creationTimestamp)
    {
        Id = id;
        UserName = userName;
        EmailAddress = emailAddress;
        PasswordHash = passwordHash;
        CreationTimestamp = creationTimestamp;
    }

    public AccountId Id { get; private set; }
    public UserName UserName { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public FullName? FullName { get; private set; }
    public DateTime CreationTimestamp { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<RefreshSession> RefreshSessions => _refreshSessions;
    public IReadOnlyList<AccountRole> AccountRoles => _accountRoles;

    public static Account CreateNew(
        UserName userName,
        EmailAddress email,
        PasswordHash passwordHash)
    {
        var id = AccountId.CreateRandom();
        var creationTimestamp = DateTime.UtcNow;

        return new Account(id, userName, email, passwordHash, creationTimestamp);
    }

    public static Result<Account, Error> Create(
        AccountId id,
        UserName userName,
        EmailAddress email,
        PasswordHash passwordHash,
        DateTime creationTimestamp)
    {
        if (creationTimestamp > DateTime.UtcNow)
            return Errors.General.ValueIsInvalid(nameof(creationTimestamp));

        return new Account(id, userName, email, passwordHash, creationTimestamp);
    }

    public Result<RefreshSession, Error> GetRefreshSessionById(RefreshSessionId refreshSessionId)
    {
        var refreshSession = _refreshSessions.FirstOrDefault(rs => rs.Id == refreshSessionId);

        if (refreshSession == null)
            return Errors.General.RecordNotFound(
                nameof(RefreshSession),
                nameof(RefreshSessionId),
                refreshSessionId.Value);

        return refreshSession;
    }

    public Result<AccountRole, Error> GetAccountRoleById(AccountRoleId accountRoleId)
    {
        var accountRole = _accountRoles.FirstOrDefault(ar => ar.Id == accountRoleId);

        if (accountRole == null)
            return Errors.General.RecordNotFound(
                nameof(AccountRole),
                nameof(AccountRoleId),
                accountRoleId.Value);

        return accountRole;
    }

    public void AddAccountRole(AccountRole accountRole)
    {
        _accountRoles.Add(accountRole);
    }
    
    public void AddAccountRoles(IEnumerable<AccountRole> accountRoles)
    {
        _accountRoles.AddRange(accountRoles);
    }

    public void AddRefreshSession(RefreshSession refreshSession)
    {
        _refreshSessions.Add(refreshSession);
    }

    public Result<Error> DeleteRefreshSession(RefreshSessionId refreshSessionId)
    {
        var refreshSession = GetRefreshSessionById(refreshSessionId);

        if (refreshSession.IsFailure)
            return refreshSession.Error;

        _refreshSessions.Remove(refreshSession.Value);
        return true;
    }

    public Result<Error> DeleteAccountRole(AccountRoleId accountRoleId)
    {
        var account = GetAccountRoleById(accountRoleId);

        if (account.IsFailure)
            return account.Error;

        _accountRoles.Remove(account.Value);
        return true;
    }
}