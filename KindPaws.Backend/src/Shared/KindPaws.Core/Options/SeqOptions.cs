namespace KindPaws.Core.Options;

public class SeqOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(SeqOptions);

    public string ConnectionString { get; init; } = null!;
    public string AspNetCoreMinimumLevel { get; init; } = null!;
    public string LogEventLevel { get; init; } = null!;
}