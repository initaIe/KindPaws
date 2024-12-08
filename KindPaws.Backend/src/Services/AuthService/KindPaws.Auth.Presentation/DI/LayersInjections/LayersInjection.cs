using KindPaws.Auth.Presentation.DI.LayersInjections.Injections;

namespace KindPaws.Auth.Presentation.DI.LayersInjections;

public static class LayersInjection
{
    public static IServiceCollection AddLayers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure();
        services.AddPresentation();

        return services;
    }
}