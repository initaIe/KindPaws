using KindPaws.Roles.Infrastructure.Seeding;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Roles.Infrastructure.DI.Injections.Seeding;

public static class SeederHostedServiceInjection
{
    public static IServiceCollection AddRolesSeederHostedService(this IServiceCollection services)
    {
        return services.AddHostedService<RolesSeederHostedService>();
    }
}