namespace KindPaws.Accounts.Infrastructure.Options;

public class AccountsSeedingOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(AccountsSeedingOptions);

    public string PermissionsPath { get; init; } = null!;
    public string RolesPath { get; init; } = null!;
    public string RolePermissionsPath { get; init; } = null!;
    public AdminCredentials AdminCredentials { get; init; } = null!;
}

public class AdminCredentials
{
    public string Email { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Role { get; init; } = null!;
}