using FluentValidation;
using KindPaws.Application.DataBase;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.PetHandlers.UpdateAdditionalInfo;

public class UpdatePetAdditionalInfoHandler
{
    private readonly ILogger<UpdatePetAdditionalInfoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdatePetAdditionalInfoCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;


    public UpdatePetAdditionalInfoHandler(
        ILogger<UpdatePetAdditionalInfoHandler> logger,
        IVolunteersRepository volunteersRepository,
        IValidator<UpdatePetAdditionalInfoCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdatePetAdditionalInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;

        var volunteerResult = await _volunteersRepository.GetByIdAsync(volunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petId = PetId.Create(command.PetId).Value;

        var petResult = volunteerResult.Value.GetPetById(petId);

        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        SupportStatus? supportStatus = null;
        if (command.SupportStatus != null)
            supportStatus = SupportStatus.Create(command.SupportStatus).Value;

        MediumDescription? description = null;
        if (command.Description != null)
            description = MediumDescription.Create(command.Description).Value;

        PetColor? color = null;
        if (command.PetColor != null)
            color = PetColor.Create(command.PetColor).Value;

        Age? age = null;
        if (command.BirthDate != null)
            age = Age.Create(command.BirthDate.Value).Value;

        MediumDescription? healthDescription = null;
        IEnumerable<Vaccine>? vaccines = null;
        IEnumerable<Disease>? diseases = null;
        HealthStatus? healthStatus = null;
        bool? isNeutered = null;

        HealthDetails? healthDetails = null;
        if (command.HealthDetails != null)
        {
            if (command.HealthDetails.Description != null)
                healthDescription = MediumDescription.Create(command.HealthDetails.Description).Value;

            if (command.HealthDetails.Vaccines != null)
                vaccines = command.HealthDetails.Vaccines.Select(v => Vaccine.Create(v).Value);

            if (command.HealthDetails.Diseases != null)
                diseases = command.HealthDetails.Diseases.Select(v => Disease.Create(v).Value);

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

        petResult.Value.UpdateAdditionalInfo(
            supportStatus,
            description,
            color,
            age,
            healthDetails,
            biometricDetails);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("PET updated additional info with ID: {petId};" +
                               " Properties: {supportStatus}, {description}, {color}, {age}, {healthDetails}, " +
                               "{biometricDetails}; " +
                               "Owner ID : {volunteerId}",
            petId.Value,
            supportStatus,
            description,
            color,
            age,
            healthDetails,
            biometricDetails,
            volunteerId.Value);

        return petId.Value;
    }
}