using FluentValidation;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Abstractions;
using KindPaws.Volunteers.Domain.AggregateRoot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.SetPetMainPhoto;

public class SetPetMainPhotoHandler
    : ICommandHandler<Guid, SetPetMainPhotoCommand>
{
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<SetPetMainPhotoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SetPetMainPhotoCommand> _validator;
    private readonly IRepository<Volunteer, VolunteerId> _volunteersRepository;

    public SetPetMainPhotoHandler(
        IFileProvider fileProvider,
        ILogger<SetPetMainPhotoHandler> logger,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork,
        IValidator<SetPetMainPhotoCommand> validator,
        IRepository<Volunteer, VolunteerId> volunteersRepository)
    {
        _fileProvider = fileProvider;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _volunteersRepository = volunteersRepository;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        SetPetMainPhotoCommand command,
        CancellationToken cancellationToken = default)
    {
        var commandValidationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!commandValidationResult.IsValid)
            return commandValidationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        var petId = PetId.Create(command.PetId).Value;
        var pet = volunteerResult.Value.GetPetById(petId).Value;

        var filePath = FilePath.Create(command.Path).Value;

        var setPetMainPhotoResult = volunteerResult.Value.SetPetMainPhoto(petId, filePath);
        if (setPetMainPhotoResult.IsFailure)
            return setPetMainPhotoResult.Error.ToErrorList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(petId, filePath, volunteerId);

        return petId.Value;
    }

    private void Log(PetId petId, FilePath filePath, VolunteerId volunteerId)
    {
        _logger.LogInformation("PET update main photo, pet with ID: {Id}; " +
                               "Main photo path: {FilePath} " +
                               "Owner ID : {VolunteerId}",
            petId,
            filePath,
            volunteerId);
    }
}