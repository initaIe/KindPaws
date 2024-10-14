using KindPaws.Application.Volunteers.Volunteer.DTOs;

namespace KindPaws.Application.Volunteers.Volunteer.UpdateAdditionalInfo.DTOs;

public record UpdateVolunteerAdditionalInfoDTO(
    string? Description,
    AddressDTO? Address,
    int? YearsOfExperience,
    IEnumerable<SocialNetworkDTO>? SocialNetworks,
    IEnumerable<RequisiteDTO>? Requisites);