using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateAdditionalInfo;

public class UpdateVolunteerAdditionalInfoHandler
    : ICommandHandler<Guid, UpdateVolunteerAdditionalInfoCommand>
{
    private readonly IEntitiesExistenceValidator<UpdateVolunteerAdditionalInfoExistenceValidationData>
        _entitiesExistenceValidator;

    private readonly ILogger<UpdateVolunteerAdditionalInfoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVolunteerAdditionalInfoCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;


    public UpdateVolunteerAdditionalInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateVolunteerAdditionalInfoHandler> logger,
        IValidator<UpdateVolunteerAdditionalInfoCommand> validator,
        IUnitOfWork unitOfWork,
        IEntitiesExistenceValidator<UpdateVolunteerAdditionalInfoExistenceValidationData> entitiesExistenceValidator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _entitiesExistenceValidator = entitiesExistenceValidator;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdateVolunteerAdditionalInfoCommand command,
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

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;
        var volunteerResult = await _volunteersRepository.GetByIdAsync(
            volunteerId,
            cancellationToken);

        MediumDescription? description = null;
        if (command.Description != null)
            description = MediumDescription.Create(command.Description).Value;

        Address? address = null;
        if (command.Address != null)
            address = Address.Create(
                command.Address.City,
                command.Address.Street).Value;

        YearsOfExperience? yearsOfExperience = null;
        if (command.YearsOfExperience != null)
            yearsOfExperience = YearsOfExperience.Create(
                command.YearsOfExperience.Value).Value;

        List<SocialNetwork> socialNetworks = [];
        if (command.SocialNetworks != null && command.SocialNetworks.Any())
            socialNetworks = command.SocialNetworks
                .Select(x => SocialNetwork.Create(x.Name, x.Link))
                .Select(x => x.Value).ToList();

        List<Requisite> requisites = [];
        if (command.Requisites != null && command.Requisites.Any())
            requisites = command.Requisites
                .Select(x => Requisite.Create(x.Name, x.Description))
                .Select(x => x.Value).ToList();

        volunteerResult.Value.UpdateAdditionalInfo(
            description,
            address,
            yearsOfExperience,
            socialNetworks,
            requisites);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Log(volunteerId, description, address, yearsOfExperience, socialNetworks, requisites);

        return volunteerId.Value;
    }

    private void Log(
        VolunteerId volunteerId,
        MediumDescription? description,
        Address? address,
        YearsOfExperience? yearsOfExperience,
        IEnumerable<SocialNetwork> socialNetworks,
        IEnumerable<Requisite> requisites)
    {
        _logger.LogInformation
        ("VOLUNTEER updated additional info with ID: {Id}; " +
         "Updated properties: {Description}, {Address}, {YearsOfExperience}, " +
         "{SocialNetworks}, {Requisites}",
            volunteerId,
            description,
            address,
            yearsOfExperience,
            socialNetworks,
            requisites);
    }
}