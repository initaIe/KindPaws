using KindPaws.Species.Infrastructure.DI;
using KindPaws.Species.Presentation.DI;

namespace KindPaws.WEB.DI.Injections.Modules;

public static class SpecieInjection
{
    /// <summary>
    /// Добавление модуля Species (Infrastructure and Presentation layers).
    /// </summary>
    public static IServiceCollection AddSpeciesModule(this IServiceCollection services)
    {
        services.AddSpeciesInfrastructure();
        services.AddSpeciesPresentation();

        return services;
    }
}