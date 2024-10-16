using FluentValidation;
using KindPaws.Application.DataBase;
using KindPaws.Application.Extensions;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.UpdateAdditionalInfo;

public class UpdateVolunteerAdditionalInfoHandler
{
    private readonly ILogger<UpdateVolunteerAdditionalInfoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVolunteerAdditionalInfoCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public UpdateVolunteerAdditionalInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateVolunteerAdditionalInfoHandler> logger,
        IValidator<UpdateVolunteerAdditionalInfoCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> HandleAsync(
        UpdateVolunteerAdditionalInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId).Value;

        var volunteerResult = await _volunteersRepository.GetByIdAsync(
            volunteerId,
            cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

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

        _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation
        ("VOLUNTEER updated additional info with ID: {VolunteerId}; " +
         "Updated properties: {Description}, {Address}, {YearsOfExperience}," +
         " {SocialNetworks}, {Requisites}",
            volunteerId.Value,
            description,
            address,
            yearsOfExperience,
            socialNetworks ?? [],
            requisites ?? []);

        return volunteerId.Value;
    }
}