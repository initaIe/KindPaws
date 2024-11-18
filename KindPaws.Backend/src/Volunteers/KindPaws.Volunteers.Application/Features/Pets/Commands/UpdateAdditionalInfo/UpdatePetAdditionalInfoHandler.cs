using FluentValidation;
using KindPaws.Core.Abstractions.DataBase;
using KindPaws.Core.Abstractions.Handlers;
using KindPaws.Core.Abstractions.Validators;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Helpers;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.BaseValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Volunteers.Application.Interfaces;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KindPaws.Volunteers.Application.Features.Pets.Commands.UpdateAdditionalInfo;

public class UpdatePetAdditionalInfoHandler
    : ICommandHandler<Guid, UpdatePetAdditionalInfoCommand>
{
    private readonly IEntitiesExistenceValidator<UpdatePetAdditionalInfoExistenceValidationData>
        _entitiesExistenceValidator;

    private readonly ILogger<UpdatePetAdditionalInfoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetAdditionalInfoCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public UpdatePetAdditionalInfoHandler(
        ILogger<UpdatePetAdditionalInfoHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<UpdatePetAdditionalInfoCommand> validator,
        [FromKeyedServices(Modules.Volunteers)]
        IUnitOfWork unitOfWork,
        IEntitiesExistenceValidator<UpdatePetAdditionalInfoExistenceValidationData> entitiesExistenceValidator)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdatePetAdditionalInfoCommand command,
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

        var supportStatus = ValueObjectsHelpers.CreateNullableValueObject(
            command.SupportStatus,
            SupportStatus.Create);

        var description = ValueObjectsHelpers.CreateNullableValueObject(
            command.Description,
            MediumString.Create);

        var color = ValueObjectsHelpers.CreateNullableValueObject(
            command.Color,
            PetColor.Create);

        var birthday = ValueObjectsHelpers.CreateNullableValueObject(
            command.Birthday,
            b => Birthday.Create(b!.Value));

        var healthDescription = ValueObjectsHelpers.CreateNullableValueObject(
            command.HealthDetails?.Description,
            MediumString.Create);

        var vaccines = ValueObjectsHelpers.CreateNullableValueObjects(
            command.HealthDetails?.Vaccines,
            Vaccine.Create);

        var diseases = ValueObjectsHelpers.CreateNullableValueObjects(
            command.HealthDetails?.Diseases,
            Disease.Create);

        var healthStatus = ValueObjectsHelpers.CreateNullableValueObject(
            command.HealthDetails?.HealthStatus,
            HealthStatus.Create);

        var healthDetails = new HealthDetails(
            healthDescription,
            vaccines,
            diseases,
            healthStatus,
            command.HealthDetails?.IsNeutered);

        var height = ValueObjectsHelpers.CreateNullableValueObject(
            command.BiometricDetails?.Height,
            h => Height.Create(h!.Value));

        var weight = ValueObjectsHelpers.CreateNullableValueObject(
            command.BiometricDetails?.Weight,
            w => Weight.Create(w!.Value));

        var gender = ValueObjectsHelpers.CreateNullableValueObject(
            command.BiometricDetails?.Gender,
            Gender.Create);

        var biometricDetails = new BiometricDetails(
            height,
            weight,
            gender);

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        var petId = PetId.Create(command.PetId).Value;
        volunteerResult.Value.UpdatePetAdditionalInfo(
            petId,
            supportStatus,
            description,
            color,
            birthday,
            healthDetails,
            biometricDetails);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(petId, supportStatus, description, color, birthday, healthDetails, biometricDetails, volunteerId);

        return petId.Value;
    }

    private void Log(
        PetId petId,
        SupportStatus? supportStatus,
        MediumString? description,
        PetColor? color,
        Birthday? age,
        HealthDetails? healthDetails,
        BiometricDetails? biometricDetails,
        VolunteerId volunteerId)
    {
        _logger.LogInformation("PET updated additional info with ID: {Id}; " +
                               "Properties: {SupportStatus}, {Description}, {Color}, {Age}, {HealthDetails}, " +
                               "{BiometricDetails}; " +
                               "Owner ID : {VolunteerId}",
            petId,
            supportStatus,
            description,
            color,
            age,
            healthDetails,
            biometricDetails,
            volunteerId);
    }
}