using KindPaws.Auth.Infrastructure.DI.Injections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Auth.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCustomOptions(configuration);
        services.AddMessaging(configuration);
        services.AddProviders();
        services.AddDataBase();
        services.AddSchedulers();

        return services;
    }
}