namespace KindPaws.Application.DTOs;

public class SpecieDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public IEnumerable<BreedDTO> Breeds { get; init; } = [];
}