using KindPaws.Volunteers.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Volunteers.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteersPresentation(this IServiceCollection services)
    {
        return services.AddScoped<IVolunteersContract, VolunteersContract>();
    }
}