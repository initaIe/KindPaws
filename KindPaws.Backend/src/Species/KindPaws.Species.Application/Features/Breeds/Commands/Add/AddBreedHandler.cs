using FluentValidation;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Application.Helpers;
using KindPaws.Species.Application.Interfaces;
using KindPaws.Species.Domain.AggregateRoot;
using KindPaws.Species.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Species.Application.Features.Breeds.Commands.Add;

public class AddBreedHandler
    : ICommandHandler<Guid, AddBreedCommand>
{
    private readonly IEntitiesExistenceValidator<AddBreedExistenceValidationData> _entitiesExistenceValidator;
    private readonly ILogger<AddBreedHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddBreedCommand> _validator;

    public AddBreedHandler(
        [FromKeyedServices(Modules.Species)] IUnitOfWork unitOfWork,
        ILogger<AddBreedHandler> logger,
        ISpeciesRepository speciesRepository,
        IValidator<AddBreedCommand> validator,
        IEntitiesExistenceValidator<AddBreedExistenceValidationData> entitiesExistenceValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _speciesRepository = speciesRepository;
        _validator = validator;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var entitiesExistenceValidationData = command.ToExistenceValidationData();
        var entitiesExistenceValidationResult = await _entitiesExistenceValidator
            .ValidateAsync(entitiesExistenceValidationData, cancellationToken);
        if (entitiesExistenceValidationResult.IsFailure)
            return entitiesExistenceValidationResult.Error.ToErrorList();

        var specieId = SpecieId.Create(command.SpecieId).Value;
        var specieResult = await _speciesRepository.GetByIdAsync(specieId, cancellationToken);

        var breed = BreedHelper.ForceCreateNewBreed(command.Name, command.Description);

        specieResult.Value.AddBreed(breed);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(specieResult.Value, breed);

        return breed.Id.Value;
    }

    private void Log(Specie specie, Breed breed)
    {
        _logger.LogInformation(
            """
            [Specie, AddBreed]
            SPECIE:
            {Specie};
            Breed:
            {Breed}
            """,
            specie,
            breed);
    }
}