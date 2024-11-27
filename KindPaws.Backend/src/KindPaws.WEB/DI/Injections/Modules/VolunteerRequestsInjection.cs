using KindPaws.VolunteerRequests.Infrastructure.DI;
using KindPaws.VolunteerRequests.Presentation.DI;

namespace KindPaws.WEB.DI.Injections.Modules;

public static class VolunteerRequestsInjection
{
    /// <summary>
    /// Добавление модуля VolunteerRequests (Infrastructure and Presentation layers).
    /// </summary>
    public static IServiceCollection AddVolunteerRequestsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddVolunteerRequestsInfrastructure(configuration);
        services.AddVolunteerRequestsPresentation();

        return services;
    }
}