using FluentValidation;
using KindPaws.Core.Abstractions.Database;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Domain.AggregateRoot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.SoftDeletePet;

public class SoftDeletePetHandler
    : ICommandHandler<Guid, SoftDeletePetCommand>
{
    private readonly ILogger<SoftDeletePetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SoftDeletePetCommand> _validator;
    private readonly IRepository<Volunteer, VolunteerId> _volunteersRepository;

    public SoftDeletePetHandler(
        ILogger<SoftDeletePetHandler> logger,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork,
        IValidator<SoftDeletePetCommand> validator,
        IRepository<Volunteer, VolunteerId> volunteersRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        SoftDeletePetCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        var petId = PetId.Create(command.PetId).Value;

        volunteerResult.Value.SoftDeletePet(petId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(petId);

        return Guid.NewGuid();
    }

    private void Log(PetId petId)
    {
        _logger.LogInformation(
            "PET soft deleted with ID: {Id}",
            petId);
    }
}