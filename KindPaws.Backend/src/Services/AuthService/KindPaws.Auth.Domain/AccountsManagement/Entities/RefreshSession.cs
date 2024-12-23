using KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.DDD;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Auth.Domain.AccountsManagement.Entities;

public class RefreshSession : Entity<RefreshSessionId>
{
    #region EF Core constructor

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RefreshSession(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        RefreshSessionId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    #endregion

    private RefreshSession(
        RefreshSessionId id,
        CreatedAt createdAt,
        Jti jti,
        RefreshToken refreshToken,
        RefreshSessionExpiresAt expiresAt)
        : base(id, createdAt)
    {
        CreatedAt = createdAt;
        Jti = jti;
        RefreshToken = refreshToken;
        ExpiresAt = expiresAt;
    }

    public Jti Jti { get; }
    public RefreshToken RefreshToken { get; }
    public RefreshSessionExpiresAt ExpiresAt { get; }

    #region Factory methods

    public static RefreshSession CreateNew(
        Jti jti,
        RefreshSessionExpiresAt expiresAt)
    {
        var id = RefreshSessionId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();
        var refreshToken = RefreshToken.CreateRandom();

        return new RefreshSession(
            id,
            createdAt,
            jti,
            refreshToken,
            expiresAt);
    }

    public static RefreshSession Create(
        RefreshSessionId id,
        CreatedAt createdAt,
        Jti jti,
        RefreshToken refreshToken,
        RefreshSessionExpiresAt expiresAt)
    {
        return new RefreshSession(
            id,
            createdAt,
            jti,
            refreshToken,
            expiresAt);
    }

    #endregion
}