using FluentValidation;
using KindPaws.Core.Abstractions;
using KindPaws.Core.Extensions;
using KindPaws.SharedKernel.Enums;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
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
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork,
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

        SupportStatus? supportStatus = null;
        if (command.SupportStatus != null)
            supportStatus = SupportStatus.Create(command.SupportStatus).Value;

        MediumDescription? description = null;
        if (command.Description != null)
            description = MediumDescription.Create(command.Description).Value;

        PetColor? color = null;
        if (command.Color != null)
            color = PetColor.Create(command.Color).Value;

        Age? age = null;
        if (command.BirthDate != null)
            age = Age.Create(command.BirthDate.Value).Value;

        MediumDescription? healthDescription = null;
        List<Vaccine> vaccines = [];
        List<Disease> diseases = [];
        HealthStatus? healthStatus = null;
        bool? isNeutered = null;

        HealthDetails? healthDetails = null;
        if (command.HealthDetails != null)
        {
            if (command.HealthDetails.Description != null)
                healthDescription = MediumDescription.Create(command.HealthDetails.Description).Value;

            if (command.HealthDetails.Vaccines != null)
                vaccines = command.HealthDetails.Vaccines.Select(v => Vaccine.Create(v).Value).ToList();

            if (command.HealthDetails.Diseases != null)
                diseases = command.HealthDetails.Diseases.Select(v => Disease.Create(v).Value).ToList();

            if (command.HealthDetails.HealthStatus != null)
                healthStatus = HealthStatus.Create(command.HealthDetails.HealthStatus).Value;

            if (command.HealthDetails.IsNeutered != null)
                isNeutered = command.HealthDetails.IsNeutered.Value;

            healthDetails = new HealthDetails(
                healthDescription,
                vaccines,
                diseases,
                healthStatus,
                isNeutered);
        }

        Height? height = null;
        Weight? weight = null;
        Gender? gender = null;

        BiometricDetails? biometricDetails = null;
        if (command.BiometricDetails != null)
        {
            if (command.BiometricDetails.Height != null)
                height = Height.Create(command.BiometricDetails.Height.Value).Value;

            if (command.BiometricDetails.Weight != null)
                weight = Weight.Create(command.BiometricDetails.Weight.Value).Value;

            if (command.BiometricDetails.Gender != null)
                gender = Gender.Create(command.BiometricDetails.Gender).Value;

            biometricDetails = new BiometricDetails(
                height,
                weight,
                gender);
        }

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        var petId = PetId.Create(command.PetId).Value;
        var petResult = volunteerResult.Value.GetPetById(petId);

        petResult.Value.UpdateAdditionalInfo(
            supportStatus,
            description,
            color,
            age,
            healthDetails,
            biometricDetails);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(petId, supportStatus, description, color, age, healthDetails, biometricDetails, volunteerId);

        return petId.Value;
    }

    private void Log(
        PetId petId,
        SupportStatus? supportStatus,
        MediumDescription? description,
        PetColor? color,
        Age? age,
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