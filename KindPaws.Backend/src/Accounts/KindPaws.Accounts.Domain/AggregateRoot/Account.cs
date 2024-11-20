using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Domain.AggregateRoot;

public sealed class Account : IEntity<AccountId>
{
    private readonly List<RefreshSession> _refreshSessions = [];
    private readonly List<SocialNetwork> _socialNetworks = [];

    // ef Core
    private Account()
    {
    }

    private Account(
        AccountId id, 
        UserName userName,
        EmailAddress email,
        PasswordHash passwordHash,
        DateTime creationTimestamp)
    {
        Id = id;
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        CreationTimestamp = creationTimestamp;
    }

    public AccountId Id { get; private set; }
    public UserName UserName { get; private set; }
    public EmailAddress Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public FullName? FullName { get; private set; }
    public DateTime CreationTimestamp { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<RefreshSession> RefreshSessions => _refreshSessions;

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
}