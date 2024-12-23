namespace KindPaws.Auth.Infrastructure.Common.Options;

public class AccountsMessagingOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(AccountsMessagingOptions);

    public string ExchangeName { get; init; } = null!;
    public string ExchangeType { get; init; } = null!;
    public bool ExchangeDurable { get; init; }
    public bool ExchangeAutoDelete { get; init; }
}