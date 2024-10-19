using KindPaws.Application.DTOs;

namespace KindPaws.Application.Volunteers.VolunteersHandlers.UpdateMainInfo;

public record UpdateVolunteerMainInfoCommand(
    Guid VolunteerId,
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber);