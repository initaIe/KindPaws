using KindPaws.Roles.Presentation.DI.Layouts.LayoutInjections;

namespace KindPaws.Roles.Presentation.DI.Layouts;

public static class LayoutInjection
{
    /// <summary>
    /// Добавляет все зависимости с всех слоев.
    /// </summary>
    public static IServiceCollection AddLayoutInjections(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);
        services.AddPresentation();

        return services;
    }
}