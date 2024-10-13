using KindPaws.Application.Volunteers.DTOs;

namespace KindPaws.Application.Volunteers.UpdateMainInfo.DTOs;

public record UpdateVolunteerMainInfoDTO(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber);