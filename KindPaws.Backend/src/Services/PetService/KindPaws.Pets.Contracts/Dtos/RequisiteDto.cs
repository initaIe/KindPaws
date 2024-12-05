namespace KindPaws.Pets.Contracts.Dtos;

public record RequisiteDto
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
}