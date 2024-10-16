using KindPaws.Application.Volunteers.DTOs;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.Create;

public record CreateVolunteerCommand(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber);