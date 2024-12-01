using KindPaws.Roles.Presentation.DI.Layouts;
using KindPaws.Roles.Presentation.DI.Others;
using KindPaws.Roles.Presentation.DI.Web;

namespace KindPaws.Roles.Presentation.DI;

public static class DependencyInjection
{
    /// <summary>
    /// Добавляет все зависимости в DI.
    /// </summary>
    public static IServiceCollection AddDependencyInjections(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddLayoutInjections(configuration);
        services.AddOtherInjections(configuration);
        services.AddWevInjections(configuration);

        return services;
    }
}