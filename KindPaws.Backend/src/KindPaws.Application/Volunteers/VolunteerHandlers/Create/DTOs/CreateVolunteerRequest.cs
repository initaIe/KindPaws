using KindPaws.Application.Volunteers.VolunteerHandlers.DTOs;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.Create.DTOs;

public record CreateVolunteerRequest(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber);