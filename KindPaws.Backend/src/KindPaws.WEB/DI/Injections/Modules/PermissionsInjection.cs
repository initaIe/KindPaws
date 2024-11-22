using KindPaws.Permissions.Infrastructure.DI;
using KindPaws.Permissions.Presentation.DI;

namespace KindPaws.WEB.DI.Injections.Modules;

public static class PermissionsInjection
{
    /// <summary>
    /// Добавление модуля Permissions (Infrastructure and Presentation layers).
    /// </summary>
    public static IServiceCollection AddPermissionsModule(this IServiceCollection services)
    {
        services.AddPermissionsInfrastructure();
        services.AddPermissionsPresentation();

        return services;
    }
}