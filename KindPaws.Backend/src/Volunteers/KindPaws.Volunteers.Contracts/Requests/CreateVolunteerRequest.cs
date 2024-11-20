using KindPaws.Volunteers.Contracts.Dtos;

namespace KindPaws.Volunteers.Contracts.Requests;

public record CreateVolunteerRequest(
    string? Description,
    AddressDto? Address,
    int? YearsOfExperience,
    IEnumerable<RequisiteDto> Requisites);