namespace KindPaws.Application.Volunteers.CreateVolunteer.DTOs;

public record CreateVolunteerRequest(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber);