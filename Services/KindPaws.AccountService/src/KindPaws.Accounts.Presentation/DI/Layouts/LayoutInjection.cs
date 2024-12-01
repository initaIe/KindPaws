using KindPaws.Accounts.Presentation.DI.Layouts.LayoutInjections;

namespace KindPaws.Accounts.Presentation.DI.Layouts;

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