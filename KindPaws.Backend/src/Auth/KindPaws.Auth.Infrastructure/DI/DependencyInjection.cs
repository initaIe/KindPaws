using KindPaws.Auth.Infrastructure.DI.Injections;
using KindPaws.Auth.Infrastructure.DI.Injections.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddProviders()
            .AddOptions(configuration)
            .AddAuth(configuration);

        return services;
    }
}