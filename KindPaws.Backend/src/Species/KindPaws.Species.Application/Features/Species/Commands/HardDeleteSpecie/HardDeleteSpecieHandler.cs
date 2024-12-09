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

namespace KindPaws.Species.Application.Features.Species.Commands.HardDeleteSpecie;

public class HardDeleteSpecieHandler : ICommandHandler<Guid, HardDeleteSpecieCommand>
{
    private readonly ILogger<HardDeleteSpecieHandler> _logger;
    private readonly IRepository<Specie, SpecieId> _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<HardDeleteSpecieCommand> _validator;

    public HardDeleteSpecieHandler(
        ILogger<HardDeleteSpecieHandler> logger,
        IRepository<Specie, SpecieId> speciesRepository,
        [FromKeyedServices(Modules.Species)] IUnitOfWork unitOfWork,
        IValidator<HardDeleteSpecieCommand> validator)
    {
        _logger = logger;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        HardDeleteSpecieCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var specieId = SpecieId.Create(command.SpecieId).Value;
        var specie = await _speciesRepository.GetByIdAsync(specieId, cancellationToken);

        _speciesRepository.Delete(specie.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(specieId);

        return specieId.Value;
    }

    private void Log(SpecieId specieId)
    {
        _logger.LogInformation(
            "SPECIE hard deleted with ID: {Id}",
            specieId);
    }
}