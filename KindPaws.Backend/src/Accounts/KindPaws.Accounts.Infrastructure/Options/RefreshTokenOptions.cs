namespace KindPaws.Accounts.Infrastructure.Options;

public class RefreshTokenOptions
{
    /// <summary>
    ///     Key name in cfg.
    /// </summary>
    public const string RefreshToken = nameof(RefreshToken);

    public int ExpiresInDays { get; init; } 
}