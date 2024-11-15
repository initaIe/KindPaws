using KindPaws.Accounts.Infrastructure.DI.Injections;
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
            // АЙДЕНТИТИ ДОБАВЛЯТЬ ПЕРЕД ПЕРЕОПРЕДЕЛЕННОЙ AUTH ИБО БУДУТ СТОЯТ ДЕФ СХЕМЫ!!!
            .AddCustomIdentity()
            .AddProviders()
            .AddDataBase()
            .AddOptions(configuration)
            .AddSeeders()
            .AddManagers()
            .AddServices()
            .AddCustomAuthentication(configuration)
            .AddCustomAuthorization()
            .AddHandlers();

        return services;
    }
}