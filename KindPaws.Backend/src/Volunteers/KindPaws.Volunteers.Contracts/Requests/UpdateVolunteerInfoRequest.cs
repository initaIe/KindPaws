namespace KindPaws.Volunteers.Contracts.Requests;

public record UpdateVolunteerInfoRequest(
    string? Description,
    AddressDto? Address,
    int? YearsOfExperience,
    IEnumerable<RequisiteDto> Requisites);