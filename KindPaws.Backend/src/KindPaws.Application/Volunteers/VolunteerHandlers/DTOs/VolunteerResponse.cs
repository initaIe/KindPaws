namespace KindPaws.Application.Volunteers.Volunteer.DTOs;

public record VolunteerResponse(
    Guid Id,
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber,
    string? Description,
    AddressDTO? Address,
    int? YearsOfExperience,
    IEnumerable<SocialNetworkDTO> SocialNetworks,
    IEnumerable<RequisiteDTO> Requisites);