namespace KindPaws.Domain.Shared.IDs;

public record PetId
{
    private PetId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }

    public static PetId NewPetId()
    {
        return new PetId(Guid.NewGuid());
    }

    public static PetId Empty()
    {
        return new PetId(Guid.Empty);
    }

    public static PetId Create(Guid id)
    {
        return new PetId(id);
    }
}