namespace KindPaws.Users.Contracts.Dtos;

public record FullNameDto
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string? Patronymic { get; init; }
}