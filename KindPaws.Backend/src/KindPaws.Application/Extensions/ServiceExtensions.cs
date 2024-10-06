using FluentValidation;
using KindPaws.Application.Volunteers.CreateVolunteer;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();

        services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);

        return services;
    }
}