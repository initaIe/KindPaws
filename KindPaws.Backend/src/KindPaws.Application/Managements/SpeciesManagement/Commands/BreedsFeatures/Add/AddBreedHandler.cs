using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Application.Helpers;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.SpeciesManagement.Commands.BreedsFeatures.Add;

public class AddBreedHandler
    : ICommandHandler<Guid, AddBreedCommand>
{
    private readonly IEntitiesExistenceChecker<AddBreedExistenceCheckData> _entitiesExistenceChecker;
    private readonly ILogger<AddBreedHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddBreedCommand> _validator;

    public AddBreedHandler(
        IUnitOfWork unitOfWork,
        ILogger<AddBreedHandler> logger,
        ISpeciesRepository speciesRepository,
        IValidator<AddBreedCommand> validator,
        IEntitiesExistenceChecker<AddBreedExistenceCheckData> entitiesExistenceChecker)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _speciesRepository = speciesRepository;
        _validator = validator;
        _entitiesExistenceChecker = entitiesExistenceChecker;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var existenceCheckData = command.ToExistenceCheckData();
        var existenceCheckerResult = await _entitiesExistenceChecker.CheckAsync(existenceCheckData, cancellationToken);
        if (existenceCheckerResult.IsFailure)
            return existenceCheckerResult.Error.ToErrorList();

        var breed = BreedHelper.ForceCreateNewBreed(
            command.Name,
            command.Description);

        var specieId = SpecieId.Create(command.SpecieId).Value;
        var specieResult = await _speciesRepository.GetByIdAsync(specieId, cancellationToken);

        specieResult.Value.AddBreed(breed);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("BREED added with ID: {breedId}; " +
                               "Properties: {name}, {description}",
            breed.Id.Value,
            breed.Name.Value,
            breed.Description.Value);

        return breed.Id.Value;
    }
}