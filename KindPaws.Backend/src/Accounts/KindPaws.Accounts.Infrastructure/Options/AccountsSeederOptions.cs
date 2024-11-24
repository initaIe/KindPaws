namespace KindPaws.Accounts.Infrastructure.Options;

public class AccountsSeederOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(AccountsSeederOptions);

    public string UserName { get; init; } = null!;
    public string EmailAddress { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string RoleName { get; init; } = null!;
}