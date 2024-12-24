namespace KindPaws.Core.Options;

public class RabbitmqOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(RabbitmqOptions);

    public string Host { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}