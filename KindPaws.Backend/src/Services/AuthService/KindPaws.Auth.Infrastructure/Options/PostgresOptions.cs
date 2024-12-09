namespace KindPaws.Auth.Infrastructure.Options;

public class PostgresOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(PostgresOptions);

    public string ConnectionString { get; set; } = null!;
}