namespace KindPaws.Permissions.Infrastructure.Options;

public class PermissionsSeederOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(PermissionsSeederOptions);

    public string PermissionsPath { get; init; } = null!;
}