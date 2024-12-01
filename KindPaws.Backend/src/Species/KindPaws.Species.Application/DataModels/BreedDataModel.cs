namespace KindPaws.Species.Application.DataModels;

public class BreedDataModel
{
    public Guid Id { get; private set; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public DateTimeOffset CreatedAt { get; init; }
    public bool IsSoftDeleted { get; init; }
    public DateTimeOffset? SoftDeletedAt { get; init; }
    public Guid SpecieId { get; init; }
}