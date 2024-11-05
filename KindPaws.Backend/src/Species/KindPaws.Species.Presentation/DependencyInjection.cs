using KindPaws.Species.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Species.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesPresentation(this IServiceCollection services)
    {
        return services.AddScoped<ISpeciesContract, SpeciesContract>();
    }
}