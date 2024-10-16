using KindPaws.Application.Volunteers.DTOs;
using KindPaws.Application.Volunteers.VolunteerHandlers.UpdateAdditionalInfo;

namespace KindPaws.API.Contracts.Volunteers.VolunteersRequests;

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