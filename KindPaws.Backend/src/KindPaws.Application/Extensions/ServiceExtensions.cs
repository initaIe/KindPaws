using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.EntitiesExistenceValidators;
using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;
using KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.HardDelete;
using KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.SoftDelete;
using KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.Create;
using KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.HardDelete;
using KindPaws.Application.Managements.SpeciesManagement.Commands.SpeciesFeatures.SoftDelete;
using KindPaws.Application.Managements.SpeciesManagement.Queries.BreedsFeatures;
using KindPaws.Application.Managements.SpeciesManagement.Queries.SpeciesFeatures;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.Add;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.AddPhotos;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.DeletePhotos;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.HardDelete;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.SoftDelete;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateAdditionalInfo;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdateMainInfo;
using KindPaws.Application.Managements.VolunteersManagement.Commands.PetsFeatures.UpdatePosition;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.Create;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.HardDelete;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.SoftDelete;
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
            .AddScoped<ICommandHandler<Guid, CreateVolunteerCommand>,
                CreateVolunteerHandler>()
            .AddScoped<ICommandHandler<Guid, SoftDeleteVolunteerCommand>,
                SoftDeleteVolunteerHandler>()
            .AddScoped<ICommandHandler<Guid, UpdateVolunteerAdditionalInfoCommand>,
                UpdateVolunteerAdditionalInfoHandler>()
            .AddScoped<ICommandHandler<Guid, UpdateVolunteerMainInfoCommand>,
                UpdateVolunteerMainInfoHandler>()
            .AddScoped<ICommandHandler<Guid, DeletePetPhotosCommand>,
                DeletePetPhotosHandler>()
            .AddScoped<ICommandHandler<Guid, AddPetCommand>,
                AddPetHandler>()
            .AddScoped<ICommandHandler<Guid, AddPetPhotosCommand>,
                AddPetPhotosHandler>()
            .AddScoped<ICommandHandler<Guid, UpdatePetAdditionalInfoCommand>,
                UpdatePetAdditionalInfoHandler>()
            .AddScoped<ICommandHandler<Guid, UpdatePetMainInfoCommand>,
                UpdatePetMainInfoHandler>()
            .AddScoped<ICommandHandler<Guid, SoftDeletePetCommand>,
                SoftDeletePetHandler>()
            .AddScoped<ICommandHandler<Guid, HardDeletePetCommand>,
                HardDeletePetHandler>()
            .AddScoped<ICommandHandler<Guid, HardDeleteVolunteerCommand>,
                HardDeleteVolunteerHandler>()
            .AddScoped<ICommandHandler<Guid, UpdatePetPositionCommand>,
                UpdatePetPositionHandler>();
    }

    private static IServiceCollection AddSpeciesCommandHandlers(this IServiceCollection services)
    {
        return services
            .AddScoped<ICommandHandler<Guid, AddBreedCommand>, AddBreedHandler>()
            .AddScoped<ICommandHandler<Guid, SoftDeleteBreedCommand>, SoftDeleteBreedHandler>()
            .AddScoped<ICommandHandler<Guid, CreateSpecieCommand>, CreateSpecieHandler>()
            .AddScoped<ICommandHandler<Guid, SoftDeleteSpecieCommand>, SoftDeleteSpecieHandler>()
            .AddScoped<ICommandHandler<Guid, HardDeleteBreedCommand>, HardDeleteBreedHandler>()
            .AddScoped<ICommandHandler<Guid, HardDeleteSpecieCommand>, HardDeleteSpecieHandler>();
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
            .AddScoped<IEntitiesExistenceValidator<SoftDeleteVolunteerExistenceValidationData>,
                SoftDeleteVolunteerEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<UpdateVolunteerAdditionalInfoExistenceValidationData>,
                UpdateVolunteerAdditionalInfoEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<UpdateVolunteerMainInfoExistenceValidationData>,
                UpdateVolunteerMainInfoEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<DeletePetPhotosExistenceValidationData>,
                DeletePetPhotosEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<SoftDeletePetExistenceValidationData>,
                SoftDeletePetEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<HardDeletePetExistenceValidationData>,
                HardDeletePetEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<HardDeleteVolunteerExistenceValidationData>,
                HardDeleteVolunteerEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<UpdatePetPositionExistenceValidationData>,
                UpdatePetPositionEntitiesExistenceValidator>();
    }

    private static IServiceCollection AddSpeciesExistenceValidators(this IServiceCollection services)
    {
        return services
            .AddScoped<IEntitiesExistenceValidator<AddBreedExistenceValidationData>,
                AddBreedEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<SoftDeleteBreedExistenceValidationData>,
                SoftDeleteBreedEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<CreateSpecieExistenceValidationData>,
                CreateSpecieEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<SoftDeleteSpecieExistenceValidationData>,
                SoftDeleteSpecieEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<HardDeleteBreedExistenceValidationData>,
                HardDeleteBreedEntitiesExistenceValidator>()
            .AddScoped<IEntitiesExistenceValidator<HardDeleteSpecieExistenceValidationData>,
                HardDeleteSpecieEntitiesExistenceValidator>();
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