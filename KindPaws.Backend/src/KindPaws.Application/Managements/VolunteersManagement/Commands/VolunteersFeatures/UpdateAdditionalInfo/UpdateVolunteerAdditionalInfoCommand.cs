using KindPaws.Application.Abstractions.Markers;
using KindPaws.Application.DTOs;

namespace KindPaws.Application.Managements.VolunteersManagement.Commands.VolunteersFeatures.UpdateAdditionalInfo;

public record UpdateVolunteerAdditionalInfoCommand(
    Guid VolunteerId,
    string? Description,
    AddressDTO? Address,
    int? YearsOfExperience,
    IEnumerable<SocialNetworkDTO>? SocialNetworks,
    IEnumerable<RequisiteDTO>? Requisites)
    : ICommand
{
    public UpdateVolunteerAdditionalInfoExistenceCheckData ToExistenceCheckData()
    {
        return new UpdateVolunteerAdditionalInfoExistenceCheckData(VolunteerId);
    }
}