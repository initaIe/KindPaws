using KindPaws.Permissions.Infrastructure.DI.Injections;
using KindPaws.Permissions.Infrastructure.DI.Injections.Seeding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Permissions.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddPermissionsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDataBase()
            // .AddSeeding()
            .AddOptions(configuration);

        return services;
    }
}