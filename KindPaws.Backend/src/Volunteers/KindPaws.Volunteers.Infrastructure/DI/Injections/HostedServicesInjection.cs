using KindPaws.Volunteers.Infrastructure.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Volunteers.Infrastructure.DI.Injections;

public static class HostedServicesInjection
{
    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        return services
            .AddHostedService<FilesCleanerBackgroundService>()
            .AddHostedService<ExpiredEntitiesCleanerBackgroundService>();
    }
}