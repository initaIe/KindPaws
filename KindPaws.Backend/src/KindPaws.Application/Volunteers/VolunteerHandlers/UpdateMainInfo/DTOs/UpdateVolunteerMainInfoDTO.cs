using KindPaws.Application.Volunteers.VolunteerHandlers.DTOs;

namespace KindPaws.Application.Volunteers.VolunteerHandlers.UpdateMainInfo.DTOs;

public record UpdateVolunteerMainInfoDTO(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber);