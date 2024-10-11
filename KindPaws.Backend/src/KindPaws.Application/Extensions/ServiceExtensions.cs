using FluentValidation;
using KindPaws.Application.Volunteers.Handlers.Create;
using KindPaws.Application.Volunteers.Handlers.Delete;
using KindPaws.Application.Volunteers.Handlers.GetById;
using KindPaws.Application.Volunteers.Handlers.UpdateAdditionalInfo;
using KindPaws.Application.Volunteers.Handlers.UpdateMainInfo;
using Microsoft.Extensions.DependencyInjection;

namespace KindPaws.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddHandlers()
            .AddValidators();

        return services;
    }
    
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services
            .AddScoped<CreateVolunteerHandler>()
            .AddScoped<UpdateVolunteerMainInfoHandler>()
            .AddScoped<DeleteVolunteerHandler>()
            .AddScoped<GetVolunteerByIdHandler>()
            .AddScoped<UpdateVolunteerAdditionalInfoHandler>();

        services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);

        return services;
    }
    
    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);

        return services;
    }
}