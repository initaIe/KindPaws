using KindPaws.VolunteerRequests.Infrastructure.DI.Injections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.VolunteerRequests.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteerRequestsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDataBase();

        return services;
    }
}