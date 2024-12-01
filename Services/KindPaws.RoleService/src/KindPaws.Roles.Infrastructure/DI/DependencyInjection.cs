using KindPaws.Roles.Infrastructure.DI.Injections;
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
            // .AddSeeding()
            .AddOptions(configuration);

        return services;
    }
}