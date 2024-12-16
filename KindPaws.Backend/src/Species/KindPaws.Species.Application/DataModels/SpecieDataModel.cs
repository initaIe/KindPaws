namespace KindPaws.Species.Application.DataModels;

public class SpecieDataModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IReadOnlyList<BreedDataModel> Breeds { get; init; } = [];
    public DateTimeOffset CreatedAt { get; init; }
    public bool IsSoftDeleted { get; init; }
    public DateTimeOffset? SoftDeletedAt { get; init; }
}