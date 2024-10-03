using KindPaws.Application.Volunteers.CreateVolunteer;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Application;

public static class Inject
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
    }
}