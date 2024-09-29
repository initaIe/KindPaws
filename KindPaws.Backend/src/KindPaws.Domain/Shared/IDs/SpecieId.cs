namespace KindPaws.Domain.Shared.IDs;

public record SpecieId
{
    private SpecieId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }

    public static SpecieId NewPetId()
    {
        return new SpecieId(Guid.NewGuid());
    }

    public static SpecieId Empty()
    {
        return new SpecieId(Guid.Empty);
    }

    public static SpecieId Create(Guid id)
    {
        return new SpecieId(id);
    }
}