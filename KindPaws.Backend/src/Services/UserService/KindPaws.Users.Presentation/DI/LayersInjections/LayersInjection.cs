using KindPaws.Users.Presentation.DI.LayersInjections.Injections;

namespace KindPaws.Users.Presentation.DI.LayersInjections;

public static class LayersInjection
{
    public static IServiceCollection AddLayers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();

        return services;
    }
}