using KindPaws.Application.Volunteers.Volunteer.UpdateAdditionalInfo.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.Volunteer.UpdateAdditionalInfo;

public class UpdateVolunteerAdditionalInfoHandler
{
    private readonly ILogger<UpdateVolunteerAdditionalInfoHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;

    public UpdateVolunteerAdditionalInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateVolunteerAdditionalInfoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> HandleAsync(
        UpdateVolunteerAdditionalInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.Create(request.VolunteerId).Value;

        var volunteerResult = await _volunteersRepository.GetByIdAsync(
            volunteerId,
            cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        MediumDescription? description = null;
        if (request.Dto.Description != null)
            description = MediumDescription.Create(request.Dto.Description).Value;

        Address? address = null;
        if (request.Dto.Address != null)
            address = Address.Create(
                request.Dto.Address.City,
                request.Dto.Address.Street).Value;

        YearsOfExperience? yearsOfExperience = null;
        if (request.Dto.YearsOfExperience != null)
            yearsOfExperience = YearsOfExperience.Create(
                request.Dto.YearsOfExperience.Value).Value;

        IEnumerable<SocialNetwork>? socialNetworks = null;
        if (request.Dto.SocialNetworks != null && request.Dto.SocialNetworks.Any())
            socialNetworks = request.Dto.SocialNetworks
                .Select(x => SocialNetwork.Create(x.Name, x.Link))
                .Select(x => x.Value);

        IEnumerable<Requisite>? requisites = null;
        if (request.Dto.Requisites != null && request.Dto.Requisites.Any())
            requisites = request.Dto.Requisites
                .Select(x => Requisite.Create(x.Name, x.Description))
                .Select(x => x.Value);

        volunteerResult.Value.UpdateAdditionalInfo(
            description,
            address,
            yearsOfExperience,
            socialNetworks,
            requisites);

        var result = await _volunteersRepository.SaveAsync(volunteerResult.Value, cancellationToken);

        _logger.LogInformation
        ("VOLUNTEER update additional info with ID: {VolunteerId}; " +
         "Updated properties: {Description}, {Address}, {YearsOfExperience}," +
         " {SocialNetworks}, {Requisites}",
            volunteerId.Value,
            description,
            address,
            yearsOfExperience,
            socialNetworks ?? [],
            requisites ?? []);

        return result;
    }
}