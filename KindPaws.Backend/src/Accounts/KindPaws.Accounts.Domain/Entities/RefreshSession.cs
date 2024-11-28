using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjects;

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
        CreationTimestamp creationTimestamp,
        RefreshSessionExpireTimestamp expireTimestamp)
    {
        Id = id;
        Jti = jti;
        RefreshToken = refreshToken;
        CreationTimestamp = creationTimestamp;
        ExpireTimestamp = expireTimestamp;
    }

    public RefreshSessionId Id { get; private set; }
    public Jti Jti { get; private set; }
    public RefreshToken RefreshToken { get; private set; }
    public CreationTimestamp CreationTimestamp { get; private set; }
    public RefreshSessionExpireTimestamp ExpireTimestamp { get; private set; }
    public bool IsExpired => DateTime.UtcNow > ExpireTimestamp.Value;

    #region Factory methods

    public static RefreshSession CreateNew(
        Jti jti,
        RefreshSessionExpireTimestamp expireTimestamp)
    {
        var id = RefreshSessionId.CreateRandom();
        var refreshToken = RefreshToken.CreateRandom();
        var creationTimestamp = CreationTimestamp.CreateNew();

        return new RefreshSession(
            id,
            jti,
            refreshToken,
            creationTimestamp,
            expireTimestamp);
    }

    #endregion
}