using KindPaws.Application.Volunteers.DTOs;

namespace KindPaws.Application.Volunteers.Create;

public record CreateVolunteerRequest(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber);