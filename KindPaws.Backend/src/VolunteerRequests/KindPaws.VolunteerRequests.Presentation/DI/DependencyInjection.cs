using KindPaws.VolunteerRequests.Contracts;
using KindPaws.VolunteerRequests.Presentation.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.VolunteerRequests.Presentation.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddVolunteerRequestsPresentation(this IServiceCollection services)
    {
        return services.AddScoped<IVolunteerRequestsContract, VolunteerRequestsContract>();
    }
}