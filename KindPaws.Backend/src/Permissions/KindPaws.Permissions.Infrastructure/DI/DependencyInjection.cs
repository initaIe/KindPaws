using KindPaws.Permissions.Infrastructure.DI.Injections;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Permissions.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddPermissionsInfrastructure(this IServiceCollection services)
    {
        services.AddDataBase();

        return services;
    }
}