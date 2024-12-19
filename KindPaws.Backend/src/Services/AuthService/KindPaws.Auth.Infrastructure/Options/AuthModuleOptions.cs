namespace KindPaws.Auth.Infrastructure.Options;

public class AuthModuleOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(AuthModuleOptions);

    public int RefreshSessionExpiresInDays { get; init; }
    public string DefaultRoleName { get; init; } = null!;
}