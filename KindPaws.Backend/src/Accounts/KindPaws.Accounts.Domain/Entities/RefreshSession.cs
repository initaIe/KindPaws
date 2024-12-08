using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Domain.Entities;

public class RefreshSession
{
    // ef core
    private RefreshSession()
    {
    }

    public RefreshSession(
        RefreshSessionId id,
        Jti jti,
        RefreshToken refreshToken,
        CreatedAt createdAt,
        RefreshSessionExpiresAt expiresAt)
    {
        Id = id;
        Jti = jti;
        RefreshToken = refreshToken;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }

    public RefreshSessionId Id { get; private set; }
    public Jti Jti { get; private set; }
    public RefreshToken RefreshToken { get; private set; }
    public CreatedAt CreatedAt { get; private set; }
    public RefreshSessionExpiresAt ExpiresAt { get; private set; }

    #region Factory methods

    public static RefreshSession CreateNew(
        Jti jti,
        RefreshSessionExpiresAt expiresAt)
    {
        var id = RefreshSessionId.CreateRandom();
        var refreshToken = RefreshToken.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new RefreshSession(
            id,
            jti,
            refreshToken,
            createdAt,
            expiresAt);
    }

    #endregion

    #region CRUD

    public bool IsExpired => DateTimeOffset.UtcNow > ExpiresAt.Value;

    #endregion
}