using KindPaws.Permissions.Infrastructure.Seeding;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Permissions.Infrastructure.DI.Injections.Seeding;

public static class SeederHostedServiceInjection
{
    public static IServiceCollection AddPermissionsSeederHostedService(this IServiceCollection services)
    {
        return services.AddHostedService<PermissionsSeederHostedService>();
    }
}