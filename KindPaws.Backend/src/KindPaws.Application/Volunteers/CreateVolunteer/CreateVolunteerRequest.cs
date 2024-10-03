namespace KindPaws.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    string FirstName,
    string LastName,
    string? Patronymic,
    string EmailAddress,
    string PhoneNumber);