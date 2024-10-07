using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Shared.ValueObjects.IDs;

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

    public static Result<PetId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid();

        return new PetId(value);
    }

    public static implicit operator Guid(PetId petId)
    {
        return petId?.Value
               ?? throw new ArgumentNullException($"{nameof(petId)} cannot be null.");
    }
}