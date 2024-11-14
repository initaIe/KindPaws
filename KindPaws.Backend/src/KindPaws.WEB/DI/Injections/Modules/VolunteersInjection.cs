using KindPaws.Volunteers.Infrastructure.DI;
using KindPaws.Volunteers.Presentation.DI;

namespace KindPaws.WEB.DI.Injections.Modules;

public static class VolunteersInjection
{
    /// <summary>
    /// Добавление модуля Volunteers (Infrastructure and Presentation layers).
    /// </summary>
    public static IServiceCollection AddVolunteersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddVolunteersInfrastructure(configuration);
        services.AddVolunteersPresentation();

        return services;
    }
}