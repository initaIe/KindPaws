namespace KindPaws.Accounts.Infrastructure.Options;

public class JwtAccessTokenOptions
{
    /// <summary>
    ///     Key name in cfg.
    /// </summary>
    public const string JwtAccessToken = nameof(JwtAccessToken);

    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string Key { get; init; } = null!;
    public string ExpiresInMinutes { get; init; } = null!;
    public bool ShouldValidateIssuer { get; init; } 
    public bool ShouldValidateAudience {get; init; } 
    public bool ShouldValidateLifetime { get; init; } 
    public bool ShouldValidateIssuerSigningKey { get; init; } 
    public int ClockSkewInMinutes { get; init; }
}