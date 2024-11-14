namespace KindPaws.Volunteers.Infrastructure.Options;

public class MinioOptions
{
    /// <summary>
    ///     Key name in cfg.
    /// </summary>
    public const string Minio = nameof(Minio);

    public string Endpoint { get; init; } = string.Empty;
    public string AccessKey { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
    public bool WithSSL { get; init; }
}