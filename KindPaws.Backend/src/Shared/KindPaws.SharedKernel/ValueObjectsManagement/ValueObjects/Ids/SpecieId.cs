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

    public static Result<SpecieId, Error> Create(Guid input)
    {
        if (GuidValidator.IsEmpty(input))
            return GeneralErrors.General.ValueIsInvalid();

        return new SpecieId(input);
    }
}