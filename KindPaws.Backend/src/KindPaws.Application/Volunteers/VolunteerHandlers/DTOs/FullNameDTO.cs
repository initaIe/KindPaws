namespace KindPaws.Application.Volunteers.Volunteer.DTOs;

public record FullNameDTO(
    string FirstName,
    string LastName,
    string? Patronymic);