using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.Models;
using KindPaws.Accounts.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace KindPaws.Accounts.Infrastructure.Providers;

public class RefreshTokenSettingsProvider : IRefreshTokenSettingsProvider
{
    private readonly RefreshTokenOptions _options;

    public RefreshTokenSettingsProvider(IOptions<RefreshTokenOptions> options)
    {
        _options = options.Value;
    }

    public RefreshTokenSettings Get()
    {
        return new RefreshTokenSettings(_options.ExpiresInDays);
    }
}