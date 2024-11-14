using KindPaws.Framework.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace KindPaws.WEB.DI.Injections.Authorization;

public static class AuthorizationInjection
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

        return services;
    }
}