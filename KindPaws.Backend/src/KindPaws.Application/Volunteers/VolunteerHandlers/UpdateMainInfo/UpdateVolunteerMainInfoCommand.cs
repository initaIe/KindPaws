using KindPaws.Application.Volunteers.DTOs;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.UpdateMainInfo;

public record UpdateVolunteerMainInfoCommand(
    Guid VolunteerId,
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber);