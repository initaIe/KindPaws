using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;

public record RefreshSession
{
    private RefreshSession(
        CreatedAt createdAt,
        Jti jti,
        RefreshToken refreshToken,
        RefreshSessionExpiresAt expiresAt)
    {
        CreatedAt = createdAt;
        Jti = jti;
        RefreshToken = refreshToken;
        ExpiresAt = expiresAt;
    }

    public CreatedAt CreatedAt { get; }
    public Jti Jti { get; }
    public RefreshToken RefreshToken { get; }
    public RefreshSessionExpiresAt ExpiresAt { get; }

    public static RefreshSession CreateNew(
        Jti jti,
        RefreshSessionExpiresAt expiresAt)
    {
        var createdAt = CreatedAt.CreateNew();
        var refreshToken = RefreshToken.CreateRandom();

        return new RefreshSession(
            createdAt,
            jti,
            refreshToken,
            expiresAt);
    }

    public static RefreshSession CreateNew(
        CreatedAt createdAt,
        Jti jti,
        RefreshToken refreshToken,
        RefreshSessionExpiresAt expiresAt)
    {
        return new RefreshSession(
            createdAt,
            jti,
            refreshToken,
            expiresAt);
    }
}