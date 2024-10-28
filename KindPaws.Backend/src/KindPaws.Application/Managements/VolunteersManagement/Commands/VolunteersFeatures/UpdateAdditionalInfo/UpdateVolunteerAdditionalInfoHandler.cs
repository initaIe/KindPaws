using FluentValidation;
using KindPaws.Application.Abstractions;
using KindPaws.Application.Abstractions.IoC;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateAdditionalInfo;

public class UpdateVolunteerAdditionalInfoHandler
    : ICommandHandler<Guid, UpdateVolunteerAdditionalInfoCommand>
{
    private readonly ILogger<UpdateVolunteerAdditionalInfoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVolunteerAdditionalInfoCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IEntitiesExistenceChecker<UpdateVolunteerAdditionalInfoExistenceCheckData> _entitiesExistenceChecker;


    public UpdateVolunteerAdditionalInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateVolunteerAdditionalInfoHandler> logger,
        IValidator<UpdateVolunteerAdditionalInfoCommand> validator,
        IUnitOfWork unitOfWork, 
        IEntitiesExistenceChecker<UpdateVolunteerAdditionalInfoExistenceCheckData> entitiesExistenceChecker)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _entitiesExistenceChecker = entitiesExistenceChecker;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdateVolunteerAdditionalInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var existenceCheckData = command.ToExistenceCheckData();
        var existenceCheckerResult = await _entitiesExistenceChecker.CheckAsync(existenceCheckData, cancellationToken);
        if (existenceCheckerResult.IsFailure)
            return existenceCheckerResult.Error.ToErrorList();
        
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

        IEnumerable<SocialNetwork>? socialNetworks = null;
        if (command.SocialNetworks != null && command.SocialNetworks.Any())
            socialNetworks = command.SocialNetworks
                .Select(x => SocialNetwork.Create(x.Name, x.Link))
                .Select(x => x.Value);

        IEnumerable<Requisite>? requisites = null;
        if (command.Requisites != null && command.Requisites.Any())
            requisites = command.Requisites
                .Select(x => Requisite.Create(x.Name, x.Description))
                .Select(x => x.Value);

        volunteerResult.Value.UpdateAdditionalInfo(
            description,
            address,
            yearsOfExperience,
            socialNetworks,
            requisites);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation
        ("VOLUNTEER updated additional info with ID: {VolunteerId}; " +
         "Updated properties: {Description}, {Address}, {YearsOfExperience}, " +
         "{SocialNetworks}, {Requisites}",
            volunteerId.Value,
            description,
            address,
            yearsOfExperience,
            socialNetworks ?? [],
            requisites ?? []);

        return volunteerId.Value;
    }
}