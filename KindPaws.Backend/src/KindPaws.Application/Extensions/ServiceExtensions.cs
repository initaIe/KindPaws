using FluentValidation;
using KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;
using KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateAdditionalInfo;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateMainInfo;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Delete;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateAdditionalInfo;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateMainInfo;
using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteerById;
using KindPaws.Application.Managements.VolunteersManagement.Queries.VolunteersFeatures.GetVolunteersWithPagination;
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
            .AddScoped<UpdateVolunteerAdditionalInfoHandler>()
            .AddScoped<AddPetHandler>()
            .AddScoped<UpdatePetMainInfoHandler>()
            .AddScoped<UpdatePetAdditionalInfoHandler>()
            .AddScoped<AddPetPhotosHandler>()
            .AddScoped<GetVolunteerByIdHandler>()
            .AddScoped<GetVolunteersWithPaginationHandler>();

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