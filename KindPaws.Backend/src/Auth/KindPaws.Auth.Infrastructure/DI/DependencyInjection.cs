using KindPaws.Auth.Infrastructure.DI.Injections.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthorizationInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddCustomAuthentication(configuration)
            .AddCustomAuthorization();

        return services;
    }
}