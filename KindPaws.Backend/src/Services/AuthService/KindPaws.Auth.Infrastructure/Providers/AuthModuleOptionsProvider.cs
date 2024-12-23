using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace KindPaws.Auth.Infrastructure.Providers;

public class AuthModuleOptionsProvider : IAuthModuleOptionsProvider
{
    private readonly IOptionsMonitor<AuthModuleOptions> _options;

    public AuthModuleOptionsProvider(IOptionsMonitor<AuthModuleOptions> options)
    {
        _options = options;
    }

    public int GetRefreshSessionExpiresInDays()
    {
        return _options.CurrentValue.RefreshSessionExpiresInDays;
    }

    public string GetDefaultRoleName()
    {
        return _options.CurrentValue.DefaultRoleName;
    }
}