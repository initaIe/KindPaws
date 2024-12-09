namespace KindPaws.Auth.Infrastructure.Options;

public class RefreshSessionOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(RefreshSessionOptions);

    public int ExpiresInDays { get; init; }
}