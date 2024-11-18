namespace KindPaws.Accounts.Contracts.Dtos;

public record FullNameDto(
    string FirstName,
    string LastName,
    string? Patronymic);