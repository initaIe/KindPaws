using KindPaws.Roles.Infrastructure.DI.Injections;
using KindPaws.Roles.Infrastructure.DI.Injections.Seeding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Roles.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddRolesInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDataBase()
            .AddOptions(configuration)
            .AddSeeding();

        return services;
    }
}