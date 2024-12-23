using KindPaws.Pets.Presentation.DI.LayersInjections;
using KindPaws.Pets.Presentation.DI.OtherInjections;
using KindPaws.Pets.Presentation.DI.WebInjections;

namespace KindPaws.Pets.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddWeb(configuration);
        services.AddOther(configuration);
        services.AddLayers(configuration);

        return services;
    }
}