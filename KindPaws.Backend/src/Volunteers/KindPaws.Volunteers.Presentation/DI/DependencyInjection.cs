using KindPaws.Volunteers.Contracts;
using KindPaws.Volunteers.Presentation.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Volunteers.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersPresentation(this IServiceCollection services)
    {
        return services.AddScoped<IVolunteersContract, VolunteersContract>();
    }
}