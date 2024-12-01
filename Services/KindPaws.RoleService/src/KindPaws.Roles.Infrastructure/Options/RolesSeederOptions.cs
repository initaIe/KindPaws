namespace KindPaws.Roles.Infrastructure.Options;

public class RolesSeederOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(RolesSeederOptions);

    public string RolesPath { get; init; } = null!;
    public string RolesPermissionsPath { get; init; } = null!;
}