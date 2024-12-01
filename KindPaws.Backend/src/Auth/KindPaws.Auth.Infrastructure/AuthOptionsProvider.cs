using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace KindPaws.Auth.Infrastructure;

public class AuthOptionsProvider : IAuthOptionsProvider
{
    private readonly IOptionsMonitor<AuthOptions> _options;

    public AuthOptionsProvider(IOptionsMonitor<AuthOptions> options)
    {
        _options = options;
    }

    public string GetDefaultAccountRoleName()
    {
        return _options.CurrentValue.CreateAccountDefaultRoleName;
    }
}