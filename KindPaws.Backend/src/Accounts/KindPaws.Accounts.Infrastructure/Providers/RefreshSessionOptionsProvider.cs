using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace KindPaws.Accounts.Infrastructure.Providers;

public class RefreshSessionOptionsProvider : IRefreshSessionOptionsProvider
{
    private readonly IOptionsMonitor<RefreshSessionOptions> _options;

    public RefreshSessionOptionsProvider(IOptionsMonitor<RefreshSessionOptions> options)
    {
        _options = options;
    }

    public int GetExpireInDays()
    {
        return _options.CurrentValue.ExpiresInDays;
    }
}