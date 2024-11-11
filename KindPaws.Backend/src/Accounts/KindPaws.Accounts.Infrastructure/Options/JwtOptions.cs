namespace KindPaws.Accounts.Infrastructure.Options;

public class JwtOptions
{
    /// <summary>
    ///     Section name.
    /// </summary>
    public const string Jwt = nameof(Jwt);
    
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string Key { get; init; }
    public string ExpiredMinutesTime { get; init; }
}