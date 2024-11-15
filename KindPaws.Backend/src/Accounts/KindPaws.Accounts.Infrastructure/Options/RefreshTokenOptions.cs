namespace KindPaws.Accounts.Infrastructure.Options;

public class RefreshTokenOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(RefreshTokenOptions);

    public int ExpiresInDays { get; init; }
}