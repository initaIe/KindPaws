using KindPaws.Auth.Presentation.DI.LayersInjections;
using KindPaws.Auth.Presentation.DI.OtherInjections;
using KindPaws.Auth.Presentation.DI.WebInjections;

namespace KindPaws.Auth.Presentation.DI;

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