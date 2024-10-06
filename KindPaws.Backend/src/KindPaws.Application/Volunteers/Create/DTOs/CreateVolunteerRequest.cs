namespace KindPaws.Application.Volunteers.Create.DTOs;

public record CreateVolunteerRequest(
    FullNameDTO FullName,
    string EmailAddress,
    string PhoneNumber);