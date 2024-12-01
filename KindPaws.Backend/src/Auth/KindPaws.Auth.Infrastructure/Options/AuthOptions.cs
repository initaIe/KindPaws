namespace KindPaws.Auth.Infrastructure.Options;

public class AuthOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(AuthOptions);

    public string CreateAccountDefaultRoleName { get; init; } = null!;
}