namespace KindPaws.Auth.Infrastructure.Common.Options;

public class RolesProducingOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(RolesProducingOptions);

    public string ExchangeName { get; init; } = null!;
    public string ExchangeType { get; init; } = null!;
    public bool ExchangeDurable { get; init; }
    public bool ExchangeAutoDelete { get; init; }
}