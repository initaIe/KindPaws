using FluentValidation;
using KindPaws.Application.Providers;
using KindPaws.Application.Volunteers.AddPet;
using KindPaws.Application.Volunteers.Create;
using KindPaws.Application.Volunteers.Delete;
using KindPaws.Application.Volunteers.GetById;
using KindPaws.Application.Volunteers.UpdateAdditionalInfo;
using KindPaws.Application.Volunteers.UpdateMainInfo;
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
        // Volunteer
        services
            .AddScoped<CreateVolunteerHandler>()
            .AddScoped<UpdateVolunteerMainInfoHandler>()
            .AddScoped<DeleteVolunteerHandler>()
            .AddScoped<GetVolunteerByIdHandler>()
            .AddScoped<UpdateVolunteerAdditionalInfoHandler>()
            .AddScoped<AddPetHandler>();

        // File
        services
            .AddScoped<FileService>();

        services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);

        return services;
    }
}