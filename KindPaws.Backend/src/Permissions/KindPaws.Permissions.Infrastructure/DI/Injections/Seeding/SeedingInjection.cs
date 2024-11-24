using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Permissions.Infrastructure.DI.Injections.Seeding;

public static class SeedingInjection
{
    public static IServiceCollection AddSeeding(this IServiceCollection services)
    {
        return services
            .AddPermissionsSeederHostedService()
            .AddPermissionsSeederService();
    }
}