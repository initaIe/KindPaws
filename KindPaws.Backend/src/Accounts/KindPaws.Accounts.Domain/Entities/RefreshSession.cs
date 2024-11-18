using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Domain.Entities;

public class RefreshSession : Entity<RefreshSessionId>
{
    // ef core
    private RefreshSession(RefreshSessionId id)
        : base(id)
    {
    }

    private RefreshSession(
        RefreshSessionId id,
        Guid userId,
        Jti jti,
        RefreshToken refreshToken,
        DateTime expireTimestamp,
        DateTime creationTimestamp)
        : base(id)
    {
        UserId = userId;
        Jti = jti;
        RefreshToken = refreshToken;
        ExpireTimestamp = expireTimestamp;
        CreationTimestamp = creationTimestamp;
    }

    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public Jti Jti { get; private set; }
    public RefreshToken RefreshToken { get; private set; }
    public DateTime ExpireTimestamp { get; private set; }
    public DateTime CreationTimestamp { get; private set; }
    public bool IsExpired => DateTime.UtcNow > ExpireTimestamp;

    public static Result<RefreshSession, Error> CreateNew(
        Guid userId,
        Jti jti,
        int expiresInDays)
    {
        if (GuidValidator.IsEmpty(userId))
            return Errors.General.ValueIsInvalid("UserId");

        var id = RefreshSessionId.CreateRandom();
        var refreshToken = RefreshToken.CreateRandom();
        var expiresIn = DateTime.UtcNow.AddDays(expiresInDays);
        var createdAt = DateTime.UtcNow;

        if (expiresIn <= createdAt)
            return Errors.General.ValueIsInvalid(nameof(ExpireTimestamp));

        return new RefreshSession(
            id,
            userId,
            jti,
            refreshToken,
            expiresIn,
            createdAt);
    }

    public static Result<RefreshSession, Error> Create(
        RefreshSessionId id,
        Guid userId,
        Jti jti,
        RefreshToken refreshToken,
        DateTime expiresIn,
        DateTime createdAt)
    {
        if (GuidValidator.IsEmpty(userId))
            return Errors.General.ValueIsInvalid("UserId");

        if (expiresIn <= createdAt)
            return Errors.General.ValueIsInvalid(nameof(ExpireTimestamp));

        return new RefreshSession(
            id,
            userId,
            jti,
            refreshToken,
            expiresIn,
            createdAt);
    }
}