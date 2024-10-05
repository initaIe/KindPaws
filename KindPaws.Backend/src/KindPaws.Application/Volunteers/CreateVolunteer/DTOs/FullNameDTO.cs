namespace KindPaws.Application.Volunteers.CreateVolunteer.DTOs;

public record FullNameDTO(
    string FirstName,
    string LastName,
    string? Patronymic);