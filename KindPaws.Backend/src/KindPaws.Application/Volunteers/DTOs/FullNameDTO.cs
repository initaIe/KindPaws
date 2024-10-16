namespace KindPaws.Application.Volunteers.DTOs;

public record FullNameDTO(
    string FirstName,
    string LastName,
    string? Patronymic);