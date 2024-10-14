using FluentValidation;
using KindPaws.Application.Providers;
using KindPaws.Application.Volunteers.PetHandlers.Add;
using KindPaws.Application.Volunteers.PetHandlers.UpdateMainInfo;
using KindPaws.Application.Volunteers.VolunteerHandlers.Create;
using KindPaws.Application.Volunteers.VolunteerHandlers.Delete;
using KindPaws.Application.Volunteers.VolunteerHandlers.GetById;
using KindPaws.Application.Volunteers.VolunteerHandlers.UpdateAdditionalInfo;
using KindPaws.Application.Volunteers.VolunteerHandlers.UpdateMainInfo;
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
            
            .AddScoped<AddPetHandler>()
            .AddScoped<UpdatePetMainInfoHandler>();

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