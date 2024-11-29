using KindPaws.Accounts.Infrastructure.DI.Injections;
using KindPaws.Accounts.Infrastructure.DI.Injections.Seeding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Accounts.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDataBase()
            .AddProviders()
            // .AddSeeding()
            .AddOptions(configuration);

        return services;
    }
}