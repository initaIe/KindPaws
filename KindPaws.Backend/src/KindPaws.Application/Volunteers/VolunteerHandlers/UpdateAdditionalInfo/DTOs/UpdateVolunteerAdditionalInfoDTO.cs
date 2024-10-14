using KindPaws.Application.Volunteers.VolunteerHandlers.DTOs;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.UpdateAdditionalInfo.DTOs;

public record UpdateVolunteerAdditionalInfoDTO(
    string? Description,
    AddressDTO? Address,
    int? YearsOfExperience,
    IEnumerable<SocialNetworkDTO>? SocialNetworks,
    IEnumerable<RequisiteDTO>? Requisites);