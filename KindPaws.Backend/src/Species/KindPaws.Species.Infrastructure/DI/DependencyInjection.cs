using KindPaws.Species.Infrastructure.DI.Injections;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Species.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesInfrastructure(this IServiceCollection services)
    {
        services.AddDataBase();

        return services;
    }
}