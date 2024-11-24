using KindPaws.Roles.Infrastructure.Seeding;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Roles.Infrastructure.DI.Injections.Seeding;

public static class SeederServiceInjection
{
    public static IServiceCollection AddRolesSeederService(this IServiceCollection services)
    {
        return services.AddScoped<RolesSeederService>();
    }
}