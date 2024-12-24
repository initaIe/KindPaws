using KindPaws.Pets.Infrastructure.DI.Injections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Pets.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCustomOptions(configuration);
        services.AddDataBase();
        services.AddSchedulers();

        return services;
    }
}