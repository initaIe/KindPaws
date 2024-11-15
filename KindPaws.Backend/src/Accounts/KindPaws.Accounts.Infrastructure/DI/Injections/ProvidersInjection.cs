using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Infrastructure.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections;

public static class ProvidersInjection
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        return services
            .AddTransient<ITokenProvider, TokenProvider>()
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
    }
}