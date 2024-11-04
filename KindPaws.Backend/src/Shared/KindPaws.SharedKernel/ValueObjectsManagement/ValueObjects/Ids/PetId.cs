using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.Utilities.ValidationManagement.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

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