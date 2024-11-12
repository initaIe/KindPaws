using KindPaws.Volunteers.Infrastructure.DI.Injections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Volunteers.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddDataBase()
            .AddHostedServices()
            .AddServices()
            .AddMessageQueues()
            .AddMinioClient(configuration)
            .AddProviders()
            .AddOptions();
    }
}