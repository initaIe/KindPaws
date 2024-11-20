using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

namespace KindPaws.Accounts.Domain.Entities;

public class RefreshSession
{
    private RefreshSession()
    {
    }

    private RefreshSession(
        RefreshSessionId id,
        Jti jti,
        RefreshToken refreshToken,
        DateTime creationTimestamp,
        DateTime expireTimestamp)
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
    public DateTime CreationTimestamp { get; private set; }
    public DateTime ExpireTimestamp { get; private set; }
    public bool IsExpired => DateTime.UtcNow > ExpireTimestamp;
}