using KindPaws.Core.Abstractions.Markers;
using KindPaws.Core.Dtos;

namespace KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateAdditionalInfo;

public record UpdateVolunteerAdditionalInfoCommand(
    Guid VolunteerId,
    string? Description,
    AddressDto? Address,
    int? YearsOfExperience,
    IEnumerable<SocialNetworkDto>? SocialNetworks,
    IEnumerable<RequisiteDto>? Requisites)
    : ICommand
{
    public UpdateVolunteerAdditionalInfoExistenceValidationData ToExistenceValidationData()
        => new(VolunteerId);
}