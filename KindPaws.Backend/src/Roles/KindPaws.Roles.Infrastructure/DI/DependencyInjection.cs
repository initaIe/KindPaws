using KindPaws.Roles.Infrastructure.DI.Injections;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Roles.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddRolesInfrastructure(this IServiceCollection services)
    {
        services.AddDataBase();

        return services;
    }
}