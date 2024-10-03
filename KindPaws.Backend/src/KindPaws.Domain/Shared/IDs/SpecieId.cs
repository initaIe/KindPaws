namespace KindPaws.Domain.Shared.IDs;

public record SpecieId
{
    private SpecieId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static SpecieId CreateRandom()
    {
        return new SpecieId(Guid.NewGuid());
    }

    public static SpecieId CreateEmpty()
    {
        return new SpecieId(Guid.Empty);
    }

    public static SpecieId Create(Guid id)
    {
        return new SpecieId(id);
    }

    public static implicit operator Guid(SpecieId specieId)
    {
        return specieId?.Value ?? throw new ArgumentNullException($"{nameof(specieId)} cannot be null.");
    }
}