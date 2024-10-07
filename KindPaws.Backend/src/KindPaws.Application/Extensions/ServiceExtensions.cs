using FluentValidation;
using KindPaws.Application.Volunteers.Handlers.Create;
using KindPaws.Application.Volunteers.Handlers.GetById;
using KindPaws.Application.Volunteers.Handlers.UpdateMainInfo;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateVolunteerMainInfoHandler>();

        services.AddScoped<GetByIdVolunteerHandler>();

        services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);

        return services;
    }
}