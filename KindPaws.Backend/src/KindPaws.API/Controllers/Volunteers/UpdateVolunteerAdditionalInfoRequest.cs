using KindPaws.Application.Volunteers.DTOs;
using KindPaws.Application.Volunteers.VolunteersHandlers.UpdateAdditionalInfo;

namespace KindPaws.API.Controllers.Volunteers;

public record UpdateVolunteerAdditionalInfoRequest(
    string? Description,
    AddressDTO? Address,
    int? YearsOfExperience,
    IEnumerable<SocialNetworkDTO>? SocialNetworks,
    IEnumerable<RequisiteDTO>? Requisites)
{
    public UpdateVolunteerAdditionalInfoCommand ToCommand(Guid id)
    {
        return new UpdateVolunteerAdditionalInfoCommand(
            id,
            Description,
            Address,
            YearsOfExperience,
            SocialNetworks,
            Requisites);
    }
}