namespace KindPaws.Application.Volunteers.VolunteerHandlers.DTOs;

public record FullNameDTO(
    string FirstName,
    string LastName,
    string? Patronymic);