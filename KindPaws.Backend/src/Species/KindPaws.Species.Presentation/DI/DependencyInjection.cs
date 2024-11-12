using KindPaws.Species.Contracts;
using KindPaws.Species.Presentation.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Species.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesPresentation(this IServiceCollection services)
    {
        return services.AddScoped<ISpeciesContract, SpeciesContract>();
    }
}