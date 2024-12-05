namespace KindPaws.Pets.Application.DataModels;

public class SpecieDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset LastModifiedAt { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IReadOnlyList<BreedDataModel> Breeds = [];
}