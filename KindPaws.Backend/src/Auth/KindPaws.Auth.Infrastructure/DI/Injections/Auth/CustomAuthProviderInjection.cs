using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections.Auth;

public static class CustomAuthProviderInjection
{
    public static IServiceCollection AddCustomAuthProviders(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
    }
}