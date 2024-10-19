namespace KindPaws.Application.DTOs;

public record FullNameDTO(
    string FirstName,
    string LastName,
    string? Patronymic);