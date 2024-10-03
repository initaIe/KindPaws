namespace KindPaws.Domain.Shared.IDs;

public record PetId
{
    private PetId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PetId CreateRandom()
    {
        return new PetId(Guid.NewGuid());
    }

    public static PetId CreateEmpty()
    {
        return new PetId(Guid.Empty);
    }

    public static PetId Create(Guid id)
    {
        return new PetId(id);
    }

    public static implicit operator Guid(PetId petId)
    {
        return petId?.Value ?? throw new ArgumentNullException($"{nameof(petId)} cannot be null.");
    }
}