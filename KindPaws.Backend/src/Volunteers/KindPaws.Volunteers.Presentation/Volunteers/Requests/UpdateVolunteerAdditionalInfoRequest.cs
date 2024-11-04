using KindPaws.Core.Dtos;
using KindPaws.Volunteers.Application.Features.Volunteers.Commands.UpdateAdditionalInfo;

namespace KindPaws.Volunteers.Presentation.Volunteers.Requests;

public record UpdateVolunteerAdditionalInfoRequest(
    string? Description,
    AddressDto? Address,
    int? YearsOfExperience,
    IEnumerable<SocialNetworkDto>? SocialNetworks,
    IEnumerable<RequisiteDto>? Requisites)
{
    public UpdateVolunteerAdditionalInfoCommand ToCommand(Guid id)
        => new(id, Description, Address, YearsOfExperience, SocialNetworks, Requisites);
}