namespace KindPaws.Application.DTOs;

public class BreedDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Guid SpecieId { get; init; }
}