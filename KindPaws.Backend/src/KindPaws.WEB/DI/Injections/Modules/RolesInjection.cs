using KindPaws.Roles.Infrastructure.DI;
using KindPaws.Roles.Presentation.DI;

namespace KindPaws.WEB.DI.Injections.Modules;

public static class RolesInjection
{
    /// <summary>
    /// Добавление модуля Roles (Infrastructure and Presentation layers).
    /// </summary>
    public static IServiceCollection AddRolesModule(this IServiceCollection services)
    {
        services.AddRolesInfrastructure();
        services.AddRolesPresentation();

        return services;
    }
}