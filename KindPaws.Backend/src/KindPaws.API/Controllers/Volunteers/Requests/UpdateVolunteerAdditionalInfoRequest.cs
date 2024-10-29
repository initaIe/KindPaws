using KindPaws.Application.DTOs;
using KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateAdditionalInfo;

namespace KindPaws.API.Controllers.Volunteers.Requests;

public record UpdateVolunteerAdditionalInfoRequest(
    string? Description,
    AddressDTO? Address,
    int? YearsOfExperience,
    IEnumerable<SocialNetworkDTO>? SocialNetworks,
    IEnumerable<RequisiteDTO>? Requisites)
{
    public UpdateVolunteerAdditionalInfoCommand ToCommand(Guid id)
        => new(id, Description, Address, YearsOfExperience, SocialNetworks, Requisites);
}