namespace KindPaws.Application.Volunteers.Create.DTOs;

public record FullNameDTO(
    string FirstName,
    string LastName,
    string? Patronymic);