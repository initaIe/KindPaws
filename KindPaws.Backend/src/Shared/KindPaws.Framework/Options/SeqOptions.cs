namespace KindPaws.Framework.Options;

public class SeqOptions
{
    /// <summary>
    ///     Key name in cfg.
    /// </summary>
    public const string Seq = nameof(Seq);

    public string ConnectionString { get; set; } = null!;
    public string AspNetCoreMinimumLevel { get; set; } = null!;
    public string LogEventLevel { get; set; } = null!;
}