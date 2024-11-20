namespace KindPaws.Species.Contracts.Dtos;

public class BreedDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public Guid SpecieId { get; init; }
    public bool IsSoftDeleted { get; init; }
}