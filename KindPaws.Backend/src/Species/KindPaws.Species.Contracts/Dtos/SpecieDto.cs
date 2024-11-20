namespace KindPaws.Species.Contracts.Dtos;

public class SpecieDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public BreedDto[] Breeds { get; init; } = [];
    public bool IsSoftDeleted { get; init; }
}