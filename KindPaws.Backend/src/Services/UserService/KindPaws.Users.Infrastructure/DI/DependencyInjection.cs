using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Users.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddRolesInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}