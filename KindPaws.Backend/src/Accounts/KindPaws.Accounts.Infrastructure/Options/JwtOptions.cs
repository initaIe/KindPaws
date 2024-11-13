namespace KindPaws.Accounts.Infrastructure.Options;

public class JwtOptions
{
    /// <summary>
    ///     Section name.
    /// </summary>
    public const string Jwt = nameof(Jwt);

    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string Key { get; init; } = null!;
    public string ExpiredMinutesTime { get; init; } = null!;
}