namespace KindPaws.Framework.Options;

public class SeqOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(SeqOptions);

    public string ConnectionString { get; set; } = null!;
    public string AspNetCoreMinimumLevel { get; set; } = null!;
    public string LogEventLevel { get; set; } = null!;
}