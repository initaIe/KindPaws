using KindPaws.Accounts.Infrastructure.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI.Injections;

public static class ManagersInjection
{
    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        return services
            .AddScoped<PermissionManager>()
            .AddScoped<AdminAccountManager>()
            .AddScoped<RolePermissionManager>();
    }
}