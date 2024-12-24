using KindPaws.Users.Presentation.DI.LayersInjections;
using KindPaws.Users.Presentation.DI.OthersInjections;
using KindPaws.Users.Presentation.DI.WebInjections;

namespace KindPaws.Users.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddWeb(configuration);
        services.AddOthers(configuration);
        services.AddLayers(configuration);

        return services;
    }
}