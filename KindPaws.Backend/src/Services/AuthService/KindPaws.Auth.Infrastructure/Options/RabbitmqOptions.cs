namespace KindPaws.Auth.Infrastructure.Options;

public class RabbitmqOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(RabbitmqOptions);

    public string Host { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string ExchangeName { get; init; } = null!;
    public string ExchangeType { get; init; } = null!;
    public bool ExchangeDurable { get; init; } 
    public bool ExchangeAutoDelete { get; init; }
}