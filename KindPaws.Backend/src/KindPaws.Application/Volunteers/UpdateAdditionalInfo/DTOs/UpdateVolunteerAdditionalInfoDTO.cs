using KindPaws.Application.Volunteers.DTOs;

namespace KindPaws.Application.Volunteers.UpdateAdditionalInfo.DTOs;

public record UpdateVolunteerAdditionalInfoDTO(
    string? Description,
    AddressDTO? Address,
    int? YearsOfExperience,
    IEnumerable<SocialNetworkDTO>? SocialNetworks,
    IEnumerable<RequisiteDTO>? Requisites);