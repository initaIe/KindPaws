using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace KindPaws.Accounts.Infrastructure.Providers;

public class RefreshSessionOptionsProvider : IRefreshSessionOptionsProvider
{
    private readonly RefreshSessionOptions _options;

    public RefreshSessionOptionsProvider(IOptions<RefreshSessionOptions> options)
    {
        _options = options.Value;
    }

    public int GetExpireInDays()
    {
        return _options.ExpiresInDays;
    }
}