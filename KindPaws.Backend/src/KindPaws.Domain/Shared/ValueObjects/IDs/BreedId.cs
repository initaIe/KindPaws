namespace KindPaws.Domain.Shared.ValueObjects.IDs;

public record BreedId
{
    private BreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static BreedId CreateRandom()
    {
        return Create(Guid.NewGuid());
    }

    public static BreedId CreateEmpty()
    {
        return Create(Guid.Empty);
    }

    public static BreedId Create(Guid id)
    {
        return new BreedId(id);
    }

    public static implicit operator Guid(BreedId breedId)
    {
        return breedId?.Value
               ?? throw new ArgumentNullException($"{nameof(breedId)} cannot be null.");
    }
}