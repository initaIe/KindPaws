using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistenceValidators;
using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;
using KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Delete;
using KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;
using KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Delete;
using KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures;
using KindPaws.Application.Managements.SpeciesManagement.Queries.SpeciesFeatures;
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
using KindPaws.Application.Models;
using KindPaws.Application.Validation.ExistValidators;
using KindPaws.Domain.Shared;
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
        return services
            .AddVolunteersCommandHandlers()
            .AddSpeciesCommandHandlers()
            .AddVolunteersQueryHandlers()
            .AddSpeciesQueryHandlers();
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        return services
            .AddFluentValidation()
            .AddEntitiesExistenceValidators()
            .AddExistenceValidators();
    }

    private static IServiceCollection AddVolunteersCommandHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<ICommandHandler<Guid, CreateVolunteerCommand>, CreateVolunteerHandler>()
            .AddScoped<ICommandHandler<Guid, DeleteVolunteerCommand>, DeleteVolunteerHandler>()
            .AddScoped<ICommandHandler<Guid, UpdateVolunteerAdditionalInfoCommand>,
                UpdateVolunteerAdditionalInfoHandler>()
            .AddScoped<ICommandHandler<Guid, UpdateVolunteerMainInfoCommand>, UpdateVolunteerMainInfoHandler>();
    }

    private static IServiceCollection AddSpeciesCommandHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<ICommandHandler<Guid, AddBreedCommand>, AddBreedHandler>()
            .AddScoped<ICommandHandler<Guid, DeleteBreedCommand>, DeleteBreedHandler>()
            .AddScoped<ICommandHandler<Guid, CreateSpecieCommand>, CreateSpecieHandler>()
            .AddScoped<ICommandHandler<Guid, DeleteSpecieCommand>, DeleteSpecieHandler>()
            .AddScoped<ICommandHandler<Guid, AddPetCommand>, AddPetHandler>()
            .AddScoped<ICommandHandler<Guid, AddPetPhotosCommand>, AddPetPhotosHandler>()
            .AddScoped<ICommandHandler<Guid, UpdatePetAdditionalInfoCommand>, UpdatePetAdditionalInfoHandler>()
            .AddScoped<ICommandHandler<Guid, UpdatePetMainInfoCommand>, UpdatePetMainInfoHandler>();
    }

    private static IServiceCollection AddVolunteersQueryHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<IQueryHandler<Result<VolunteerDTO, ErrorList>, GetVolunteerByIdQuery>,
                GetVolunteerByIdHandler>()
            .AddScoped<IQueryHandler<PagedList<VolunteerDTO>, GetVolunteersWithPaginationQuery>,
                GetVolunteersWithPaginationHandler>();
    }

    private static IServiceCollection AddSpeciesQueryHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<IQueryHandler<PagedList<BreedDTO>, GetBreedsBySpecieIdWithPaginationQuery>,
                GetBreedsBySpecieIdWithPaginationHandler>()
            .AddScoped<IQueryHandler<PagedList<SpecieDTO>, GetSpeciesWithPaginationQuery>,
                GetSpeciesWithPaginationHandler>();
    }
    
    private static IServiceCollection AddExistenceValidators(this IServiceCollection services)
    {
        return services
            .AddVolunteersExistenceValidators()
            .AddSpeciesExistenceValidators();
    }

    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssembly(typeof(ServiceExtensions).Assembly);
    }

    private static IServiceCollection AddVolunteersExistenceValidators(this IServiceCollection services)
    {
        return services
            .AddScoped<IEntitiesExistenceValidator<AddPetExistenceValidationData>,
                AddPetEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<AddPetPhotosExistenceValidationData>,
                AddPetPhotosEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<UpdatePetAdditionalInfoExistenceValidationData>,
                UpdatePetAdditionalInfoEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<UpdatePetMainInfoExistenceValidationData>,
                UpdatePetMainInfoEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<CreateVolunteerExistenceValidationData>,
                CreateVolunteerEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<DeleteVolunteerExistenceValidationData>,
                DeleteVolunteerEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<UpdateVolunteerAdditionalInfoExistenceValidationData>,
                UpdateVolunteerAdditionalInfoEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<UpdateVolunteerMainInfoExistenceValidationData>,
                UpdateVolunteerMainInfoEntitiesExistenceValidator>();
    }

    private static IServiceCollection AddSpeciesExistenceValidators(this IServiceCollection services)
    {
        return services
            .AddScoped<IEntitiesExistenceValidator<AddBreedExistenceValidationData>,
                AddBreedEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<DeleteBreedExistenceValidationData>,
                DeleteBreedEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<CreateSpecieExistenceValidationData>,
                CreateSpecieEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<DeleteSpecieExistenceValidationData>,
                DeleteSpecieEntitiesExistenceValidator>();
    }
    
    private static IServiceCollection AddEntitiesExistenceValidators(this IServiceCollection services)
    {
        return services
            .AddScoped<IBreedExistenceValidator, BreedExistenceValidator>()
            .AddScoped<ISpecieExistenceValidator, SpecieExistenceValidator>()
            .AddScoped<IPetExistenceValidator, PetExistenceValidator>()
            .AddScoped<IVolunteerExistenceValidator, VolunteerExistenceValidator>();

    }

}