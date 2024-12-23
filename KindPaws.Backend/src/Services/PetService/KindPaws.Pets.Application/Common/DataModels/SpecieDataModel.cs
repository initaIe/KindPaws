namespace KindPaws.Pets.Application.Common.DataModels;

public class SpecieDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? LastModifiedAt { get; init; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Name { get; init; } = null!;

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Description { get; init; } = null!;
    public IReadOnlyList<BreedDataModel> Breeds = [];
}