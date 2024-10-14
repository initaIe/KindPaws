using KindPaws.Application.Volunteers.Volunteer.DTOs;

namespace KindPaws.Application.Volunteers.Volunteer.Create.DTOs;

public record CreateVolunteerRequest(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber);