using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI.Injections.Auth;

public static class CustomAuthHandlersInjection
{
    public static IServiceCollection AddCustomAuthHandlers(this IServiceCollection services)
    {
        return services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
    }
}