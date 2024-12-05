using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record BreedId
{
    private BreedId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static BreedId CreateRandom()
    {
        return new BreedId(Guid.NewGuid());
    }

    public static Result<BreedId, Error> Create(Guid input)
    {
        if (GuidValidator.IsEmpty(input))
            return Errors.General.ValueIsInvalid();

        return new BreedId(input);
    }
}