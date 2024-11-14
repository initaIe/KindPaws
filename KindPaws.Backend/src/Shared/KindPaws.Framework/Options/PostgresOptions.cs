namespace KindPaws.Framework.Options;

public class PostgresOptions
{
    /// <summary>
    ///     Key name in cfg.
    /// </summary>
    public const string Postgres = nameof(Postgres);

    public string ConnectionString { get; set; } = null!;
}