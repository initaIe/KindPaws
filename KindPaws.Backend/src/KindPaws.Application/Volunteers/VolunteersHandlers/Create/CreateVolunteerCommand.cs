using KindPaws.Application.Volunteers.DTOs;

namespace KindPaws.Application.Volunteers.VolunteersHandlers.Create;

public record CreateVolunteerCommand(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber);