using KindPaws.Users.Infrastructure.DI.Injections;
using KindPaws.Users.Infrastructure.DI.Injections.MessagingInjections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Users.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCustomOptions(configuration);
        services.AddDataBase();
        services.AddSchedulers();
        services.AddMessaging(configuration);

        return services;
    }
}