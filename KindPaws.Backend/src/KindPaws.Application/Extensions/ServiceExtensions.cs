using FluentValidation;
using KindPaws.Application.Species.BreedsHandlers.Add;
using KindPaws.Application.Species.SpeciesHandlers.Create;
using KindPaws.Application.Volunteers.PetsHandlers.Add;
using KindPaws.Application.Volunteers.PetsHandlers.AddPhotos;
using KindPaws.Application.Volunteers.PetsHandlers.UpdateAdditionalInfo;
using KindPaws.Application.Volunteers.PetsHandlers.UpdateMainInfo;
using KindPaws.Application.Volunteers.VolunteersHandlers.Create;
using KindPaws.Application.Volunteers.VolunteersHandlers.Delete;
using KindPaws.Application.Volunteers.VolunteersHandlers.GetById;
using KindPaws.Application.Volunteers.VolunteersHandlers.UpdateAdditionalInfo;
using KindPaws.Application.Volunteers.VolunteersHandlers.UpdateMainInfo;
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
        // Volunteers
        services
            .AddScoped<CreateVolunteerHandler>()
            .AddScoped<UpdateVolunteerMainInfoHandler>()
            .AddScoped<DeleteVolunteerHandler>()
            .AddScoped<GetVolunteerByIdHandler>()
            .AddScoped<UpdateVolunteerAdditionalInfoHandler>()
            .AddScoped<AddPetHandler>()
            .AddScoped<UpdatePetMainInfoHandler>()
            .AddScoped<UpdatePetAdditionalInfoHandler>()
            .AddScoped<AddPetPhotosHandler>();

        // Species
        services
            .AddScoped<CreateSpecieHandler>()
            .AddScoped<AddBreedHandler>();

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);

        return services;
    }
}