using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.Auth.Domain.RolesManagement.AggregateRoot;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Auth.Domain.AccountsManagement.AggregateRoot;

public class Account : AggregateRoot<AccountId>
{
    private List<RefreshSession> _refreshSessions = [];
    private List<AccountRoleId> _roles = [];

    #region EF Core constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Account(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        AccountId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    #endregion

    private Account(
        AccountId id,
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
    public PhoneNumber? PhoneNumber { get; private set; }
    public IReadOnlyList<AccountRoleId> Roles => _roles;
    public IReadOnlyList<RefreshSession> RefreshSessions => _refreshSessions;

    #region Factory methods

    public static Account CreateNew(
        UserName userName,
        EmailAddress emailAddress)
    {
        var id = AccountId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new Account(
            id,
            createdAt,
            userName,
            emailAddress);
    }

    public static Account Create(
        AccountId id,
        CreatedAt createdAt,
        UserName userName,
        EmailAddress emailAddress)
    {
        return new Account(
            id,
            createdAt,
            userName,
            emailAddress);
    }

    #endregion

    #region Account CRUD

    public void UpdateUserName(UserName userName)
    {
        UpdateLastModifiedAt();
        UserName = userName;
    }

    public void UpdateEmailAddress(EmailAddress emailAddress)
    {
        UpdateLastModifiedAt();
        EmailAddress = emailAddress;
    }

    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        UpdateLastModifiedAt();
        PhoneNumber = phoneNumber;
    }

    #endregion

    #region Roles CRUD

    public bool HasRole(AccountRoleId accountRoleId)
        => _roles.Contains(accountRoleId);

    public Result<Error> AddRole(AccountRoleId accountRoleId)
    {
        var isRoleAlreadyExist = HasRole(accountRoleId);

        if (isRoleAlreadyExist)
            return GeneralErrors.General.RecordAlreadyExist(nameof(Role), nameof(AccountRoleId));

        _roles.Add(accountRoleId);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> AddRoles(IEnumerable<AccountRoleId> accountRoleIds)
    {
        foreach (var accountRoleId in accountRoleIds)
        {
            var addRoleResult = AddRole(accountRoleId);

            if (addRoleResult.IsFailure)
                return addRoleResult.Error;
        }

        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> DeleteRole(AccountRoleId accountRoleId)
    {
        var isRoleExist = HasRole(accountRoleId);

        if (!isRoleExist)
            return GeneralErrors.General.RecordNotFound(nameof(Role), nameof(AccountRoleId));

        _roles.Remove(accountRoleId);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> DeleteRoles(IEnumerable<AccountRoleId> accountRoleIds)
    {
        foreach (var accountRoleId in accountRoleIds)
        {
            var deleteRoleResult = DeleteRole(accountRoleId);

            if (deleteRoleResult.IsFailure)
                return deleteRoleResult.Error;
        }

        UpdateLastModifiedAt();
        return true;
    }

    #endregion

    #region RefreshSessions CRUD

    public bool HasRefreshSession(RefreshSession refreshSession)
        => _refreshSessions.Contains(refreshSession);

    public Result<Error> AddRefreshSession(RefreshSession refreshSession)
    {
        var isRefreshSessionAlreadyExist = HasRefreshSession(refreshSession);

        if (isRefreshSessionAlreadyExist)
            return GeneralErrors.General.RecordAlreadyExist(nameof(Role), nameof(AccountRoleId));

        _refreshSessions.Add(refreshSession);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> AddRefreshSessions(IEnumerable<RefreshSession> refreshSessions)
    {
        foreach (var refreshSession in refreshSessions)
        {
            var addRefreshSessionResult = AddRefreshSession(refreshSession);

            if (addRefreshSessionResult.IsFailure)
                return addRefreshSessionResult.Error;
        }

        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> DeleteRefreshSession(RefreshSession refreshSession)
    {
        var isRefreshSessionExist = HasRefreshSession(refreshSession);

        if (!isRefreshSessionExist)
            return GeneralErrors.General.RecordNotFound(nameof(RefreshSession));

        _refreshSessions.Remove(refreshSession);
        UpdateLastModifiedAt();
        return true;
    }

    public Result<Error> DeleteRefreshSessions(IEnumerable<RefreshSession> refreshSessions)
    {
        foreach (var refreshSession in refreshSessions)
        {
            var deleteRefreshSessionsResult = DeleteRefreshSession(refreshSession);

            if (deleteRefreshSessionsResult.IsFailure)
                return deleteRefreshSessionsResult.Error;
        }

        UpdateLastModifiedAt();
        return true;
    }

    #endregion
}