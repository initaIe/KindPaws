using KindPaws.Application.Volunteers.Volunteer.DTOs;

namespace KindPaws.Application.Volunteers.Volunteer.UpdateMainInfo.DTOs;

public record UpdateVolunteerMainInfoDTO(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber);