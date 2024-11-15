namespace KindPaws.Accounts.Infrastructure.Options;

public class JwtBearerOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(JwtBearerOptions);

    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string Key { get; init; } = null!;
    public int ExpiresInMinutes { get; init; } 
    public bool ShouldValidateIssuer { get; init; }
    public bool ShouldValidateAudience { get; init; }
    public bool ShouldValidateLifetime { get; init; }
    public bool ShouldValidateIssuerSigningKey { get; init; }
    public int ClockSkewInMinutes { get; init; }
}