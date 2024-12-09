using FluentValidation;
using KindPaws.Core.Abstractions.Database;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Species.Domain.AggregateRoot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Species.Application.Features.Breeds.Commands.SoftDeleteBreed;

public class SoftDeleteBreedHandler : ICommandHandler<Guid, SoftDeleteBreedCommand>
{
    private readonly ILogger<SoftDeleteBreedHandler> _logger;
    private readonly IRepository<Specie, SpecieId> _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SoftDeleteBreedCommand> _validator;

    public SoftDeleteBreedHandler(
        ILogger<SoftDeleteBreedHandler> logger,
        IRepository<Specie, SpecieId> speciesRepository,
        [FromKeyedServices(Modules.Species)] IUnitOfWork unitOfWork,
        IValidator<SoftDeleteBreedCommand> validator)
    {
        _logger = logger;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        SoftDeleteBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var specieId = SpecieId.Create(command.SpecieId).Value;
        var specieResult = await _speciesRepository.GetByIdAsync(specieId, cancellationToken);

        var breedId = BreedId.Create(command.BreedId).Value;

        specieResult.Value.SoftDeleteBreed(breedId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(breedId);

        return breedId.Value;
    }

    private void Log(BreedId breedId)
    {
        _logger.LogInformation(
            "BREED soft deleted with ID: {Id}",
            breedId);
    }
}