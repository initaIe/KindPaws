namespace KindPaws.Accounts.Infrastructure.Options;

public class AdminSeedOptions
{
    /// <summary>
    ///     Key name in cfg.
    /// </summary>
    public const string AdminCredentials = nameof(AdminCredentials);

    public string Email { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
}