using FluentValidation;
using KindPaws.Application.Extensions;
using KindPaws.Application.Volunteers.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.GetById;

public class GetVolunteerByIdHandler
{
    private readonly IValidator<GetVolunteerByIdCommand> _validator;
    private readonly IVolunteersRepository _volunteersRepository;

    public GetVolunteerByIdHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<GetVolunteerByIdCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
    }

    public async Task<Result<VolunteerResponse, ErrorList>> HandleAsync(
        GetVolunteerByIdCommand command,
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
            socialNetworks = volunteerResult.Value.SocialNetworks
                .Select<SocialNetwork, SocialNetworkDTO>(x => new SocialNetworkDTO(x.Name, x.Link));

        IEnumerable<RequisiteDTO> requisites = [];
        if (volunteerResult.Value.Requisites is { Count: > 0 })
            requisites = volunteerResult.Value.Requisites
                .Select<Requisite, RequisiteDTO>(x => new RequisiteDTO(x.Name, x.Description));

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