using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

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

    public static Result<SpecieId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid();

        return new SpecieId(value);
    }

    public static implicit operator Guid(SpecieId specieId)
    {
        return specieId?.Value
               ?? throw new ArgumentNullException($"{nameof(specieId)} cannot be null.");
    }
}