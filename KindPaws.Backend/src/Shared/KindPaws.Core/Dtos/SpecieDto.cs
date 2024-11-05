namespace KindPaws.Core.Dtos;

public class SpecieDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public BreedDto[] Breeds { get; init; }
    public bool IsSoftDeleted { get; init; }
}