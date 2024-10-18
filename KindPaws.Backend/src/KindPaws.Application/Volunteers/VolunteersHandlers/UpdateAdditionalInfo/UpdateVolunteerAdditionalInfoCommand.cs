using KindPaws.Application.Volunteers.DTOs;

namespace KindPaws.Application.Volunteers.VolunteersHandlers.UpdateAdditionalInfo;

public record UpdateVolunteerAdditionalInfoCommand(
    Guid VolunteerId,
    string? Description,
    AddressDTO? Address,
    int? YearsOfExperience,
    IEnumerable<SocialNetworkDTO>? SocialNetworks,
    IEnumerable<RequisiteDTO>? Requisites);