using KindPaws.Application.Volunteers.Volunteer.DTOs;
using KindPaws.Application.Volunteers.Volunteer.GetById.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;
using Microsoft.Extensions.Logging;

namespace KindPaws.Application.Volunteers.Volunteer.GetById;

public class GetVolunteerByIdHandler
{
    private readonly ILogger<GetVolunteerByIdHandler> _logger;
    private readonly IVolunteersRepository _volunteersRepository;

    public GetVolunteerByIdHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<GetVolunteerByIdHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<VolunteerResponse, Error>> HandleAsync(
        GetVolunteerByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.Create(request.VolunteerId).Value;

        var volunteerResult = await _volunteersRepository.GetByIdAsync(
            volunteerId,
            cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var fullName = new FullNameDTO(
            volunteerResult.Value.FullName.FirstName,
            volunteerResult.Value.FullName.LastName,
            volunteerResult.Value.FullName.Patronymic);

        AddressDTO? address = null;
        if (volunteerResult.Value.Address != null)
            address = new AddressDTO(
                volunteerResult.Value.Address.City,
                volunteerResult.Value.Address.Street);

        string? description = null;
        if (volunteerResult.Value.Description != null) description = volunteerResult.Value.Description.Value;

        int? yearsOfExperience = null;
        if (volunteerResult.Value.YearsOfExperience != null)
            yearsOfExperience = volunteerResult.Value.YearsOfExperience.Value;

        IEnumerable<SocialNetworkDTO> socialNetworks = [];
        if (volunteerResult.Value.SocialNetworks is { Count: > 0 })
            socialNetworks = Enumerable
                .Select<SocialNetwork, SocialNetworkDTO>(volunteerResult.Value.SocialNetworks, x => new SocialNetworkDTO(x.Name, x.Link));

        IEnumerable<RequisiteDTO> requisites = [];
        if (volunteerResult.Value.Requisites is { Count: > 0 })
            requisites = Enumerable
                .Select<Requisite, RequisiteDTO>(volunteerResult.Value.Requisites, x => new RequisiteDTO(x.Name, x.Description));

        var volunteerResponse = new VolunteerResponse(
            volunteerResult.Value.Id,
            fullName,
            volunteerResult.Value.EmailAddress.Value,
            volunteerResult.Value.PhoneNumber.Value,
            description,
            address,
            yearsOfExperience,
            socialNetworks,
            requisites);

        return volunteerResponse;
    }
}