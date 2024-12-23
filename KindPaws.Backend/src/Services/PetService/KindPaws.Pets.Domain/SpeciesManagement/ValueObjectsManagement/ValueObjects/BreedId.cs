using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjects;

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
            return ErrorsGeneral.ValueIsInvalid();

        return new BreedId(input);
    }
}