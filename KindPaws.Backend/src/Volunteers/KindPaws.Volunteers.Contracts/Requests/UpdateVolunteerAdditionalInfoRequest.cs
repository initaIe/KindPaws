using KindPaws.Core.Dtos;

namespace KindPaws.Volunteers.Contracts.Requests;

public record UpdateVolunteerAdditionalInfoRequest(
    string? Description,
    AddressDto? Address,
    int? YearsOfExperience,
    IEnumerable<SocialNetworkDto>? SocialNetworks,
    IEnumerable<RequisiteDto>? Requisites);