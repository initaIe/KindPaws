using FluentValidation;
using KindPaws.Core.Abstractions.Database;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Application.Helpers;
using KindPaws.Volunteers.Domain.AggregateRoot;
using KindPaws.Volunteers.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.AddPet;

public class AddPetHandler
    : ICommandHandler<Guid, AddPetCommand>
{
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetCommand> _validator;
    private readonly IRepository<Volunteer, VolunteerId> _volunteersRepository;
    private readonly IVolunteersLockService _volunteersLockService;


    public AddPetHandler(
        ILogger<AddPetHandler> logger,
        IRepository<Volunteer, VolunteerId> volunteersRepository,
        IValidator<AddPetCommand> validator,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork,
        IVolunteersLockService volunteersLockService)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _volunteersLockService = volunteersLockService;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;

        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        var pet = PetHelper.ForceCreateNewPet(command.Name, command.SpecieId, command.BreedId);

        var addPetResult = volunteerResult.Value.AddPet(pet);
        if (addPetResult.IsFailure)
            return addPetResult.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(pet, volunteerId);

        return pet.Id.Value;
    }

    private void Log(Pet pet, VolunteerId volunteerId)
    {
        _logger.LogInformation(
            "PET created with ID: {Id}; " +
            "Properties: {PetType}, {PetName}; " +
            "Owner ID : {VolunteerId}",
            pet.Id,
            pet.PetType,
            pet.Name,
            volunteerId);
    }
}