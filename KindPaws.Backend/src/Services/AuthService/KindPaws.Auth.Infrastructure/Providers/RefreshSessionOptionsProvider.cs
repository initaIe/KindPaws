using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace KindPaws.Auth.Infrastructure.Providers;

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