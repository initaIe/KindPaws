namespace KindPaws.Users.Infrastructure.Common.Options;

public class AccountsConsumingOptions
{
    /// <summary>
    ///     Section name in IConfiguration.
    /// </summary>
    public const string SectionName = nameof(AccountsConsumingOptions);

    public string ExchangeName { get; init; } = null!;
    public string ExchangeType { get; init; } = null!;
    public bool ExchangeDurable { get; init; }
    public bool ExchangeAutoDelete { get; init; }
}