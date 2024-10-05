using FluentValidation;
using KindPaws.Application.Volunteers.CreateVolunteer;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();

        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

        return services;
    }
}