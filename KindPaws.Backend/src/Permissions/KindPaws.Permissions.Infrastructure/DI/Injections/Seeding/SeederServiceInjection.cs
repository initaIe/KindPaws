using KindPaws.Permissions.Infrastructure.Seeding;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Permissions.Infrastructure.DI.Injections.Seeding;

public static class SeederServiceInjection
{
    public static IServiceCollection AddPermissionsSeederService(this IServiceCollection services)
    {
        return services.AddScoped<PermissionsSeederService>();
    }
}