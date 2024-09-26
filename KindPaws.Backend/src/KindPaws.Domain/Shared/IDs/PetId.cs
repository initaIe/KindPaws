namespace KindPaws.Domain.Shared.IDs;

public record PetId
{
    private PetId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }

    public static PetId Create(Guid value)
    {
        return new PetId(value);
    }

    public static PetId RandomPetId()
    {
        return Create(Guid.NewGuid());
    }
    
    public static PetId EmptyPetId()
    {
        return Create(Guid.Empty);
    }
}