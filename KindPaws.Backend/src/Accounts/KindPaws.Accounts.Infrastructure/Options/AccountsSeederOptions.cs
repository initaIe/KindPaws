namespace KindPaws.Accounts.Infrastructure.Options;

public class AccountsSeederOptions
{
    /// <summary>
    ///     Key name in cfg.
    /// </summary>
    public const string AccountsSeeder = nameof(AccountsSeeder);

    public string PermissionsPath { get; init; } = null!;
    public string RolesPath { get; init; } = null!;
    public string RolePermissionsPath { get; init; } = null!;
    public string AdminEmail { get; init; } = null!;
    public string AdminUserName { get; init; } = null!;
    public string AdminPassword { get; init; } = null!;
}