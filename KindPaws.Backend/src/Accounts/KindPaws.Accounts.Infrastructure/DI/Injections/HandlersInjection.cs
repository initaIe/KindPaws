using KindPaws.Accounts.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections;

public static class HandlersInjection
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        return services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
    }
}