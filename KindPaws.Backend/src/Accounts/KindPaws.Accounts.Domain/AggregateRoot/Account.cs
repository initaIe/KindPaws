using KindPaws.Accounts.Domain.Entities;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Domain.AggregateRoot;

public sealed class Account : Entity<AccountId>
{
    private readonly List<RefreshSession> _refreshSessions = [];
    private List<UserRoleId> _roles = [];
    private List<SocialNetwork> _socialNetworks = [];

    // ef core
    private Account()
    {
    }

    public Account(
        AccountId id,
        UserName userName,
        EmailAddress emailAddress,
        PasswordHash passwordHash,
        CreatedAt createdAt)
    {
        Id = id;
        UserName = userName;
        EmailAddress = emailAddress;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }

    public AccountId Id { get; private set; }
    public UserName UserName { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public FullName? FullName { get; private set; }
    public CreatedAt CreatedAt { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<RefreshSession> RefreshSessions => _refreshSessions;
    public IReadOnlyList<UserRoleId> Roles => _roles;

    #region Factory methods

    public static Account CreateNew(
        UserName userName,
        EmailAddress email,
        PasswordHash passwordHash)
    {
        var id = AccountId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();
        return new Account(id, userName, email, passwordHash, createdAt);
    }

    #endregion

    #region CRUD

    public Result<RefreshSession, Error> GetRefreshSessionById(RefreshSessionId refreshSessionId)
    {
        var refreshSession = _refreshSessions.FirstOrDefault(rs => rs.Id == refreshSessionId);

        if (refreshSession == null)
            return GeneralErrors.RecordNotFound(
                nameof(RefreshSession),
                nameof(RefreshSessionId),
                refreshSessionId.Value);

        return refreshSession;
    }

    public void AddRole(UserRoleId userRoleId)
    {
        _roles.Add(userRoleId);
    }

    public void AddRoles(IEnumerable<UserRoleId> roleIds)
    {
        _roles.AddRange(roleIds);
    }

    public void AddRefreshSession(RefreshSession refreshSession)
    {
        _refreshSessions.Add(refreshSession);
    }

    public void AddRefreshSessions(IEnumerable<RefreshSession> refreshSessions)
    {
        _refreshSessions.AddRange(refreshSessions);
    }

    public void DeleteRefreshSession(RefreshSessionId refreshSessionId)
    {
        var getRefreshSessionResult = GetRefreshSessionById(refreshSessionId);

        if (getRefreshSessionResult.IsFailure)
            return;

        _refreshSessions.Remove(getRefreshSessionResult.Value);
    }

    public void DeleteRefreshSessions(IEnumerable<RefreshSessionId> refreshSessionIds)
    {
        foreach (var refreshSessionId in refreshSessionIds)
        {
            var getRefreshSessionResult = GetRefreshSessionById(refreshSessionId);

            if (getRefreshSessionResult.IsFailure)
                continue;

            _refreshSessions.Remove(getRefreshSessionResult.Value);
        }
    }

    public void DeleteRole(UserRoleId userRoleId)
    {
        _roles.Remove(userRoleId);
    }

    public void DeleteRoles(IEnumerable<UserRoleId> roleIds)
    {
        foreach (var roleId in roleIds)
        {
            _roles.Remove(roleId);
        }
    }

    #endregion
}