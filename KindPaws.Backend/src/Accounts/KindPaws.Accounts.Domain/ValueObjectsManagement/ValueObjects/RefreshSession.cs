using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;

public record RefreshSession
{
    private RefreshSession(
        Jti jti,
        RefreshToken refreshToken,
        DateTime expireTimestamp,
        DateTime creationTimestamp)
    {
        Jti = jti;
        RefreshToken = refreshToken;
        ExpireTimestamp = expireTimestamp;
        CreationTimestamp = creationTimestamp;
    }
    
    public Jti Jti { get; private set; }
    public RefreshToken RefreshToken { get; private set; }
    public DateTime ExpireTimestamp { get; private set; }
    public DateTime CreationTimestamp { get; private set; }
    public bool IsExpired => DateTime.UtcNow > ExpireTimestamp;

    public static Result<RefreshSession, Error> CreateNew(
        Jti jti,
        int expiresInDays)
    {

        var refreshToken = RefreshToken.CreateRandom();
        var expiresIn = DateTime.UtcNow.AddDays(expiresInDays);
        var createdAt = DateTime.UtcNow;

        if (expiresIn <= createdAt)
            return Errors.General.ValueIsInvalid(nameof(ExpireTimestamp));

        return new RefreshSession(
            jti,
            refreshToken,
            expiresIn,
            createdAt);
    }

    public static Result<RefreshSession, Error> Create(
        Jti jti,
        RefreshToken refreshToken,
        DateTime expiresIn,
        DateTime createdAt)
    {

        if (expiresIn <= createdAt)
            return Errors.General.ValueIsInvalid(nameof(ExpireTimestamp));

        return new RefreshSession(
            jti,
            refreshToken,
            expiresIn,
            createdAt);
    }

}