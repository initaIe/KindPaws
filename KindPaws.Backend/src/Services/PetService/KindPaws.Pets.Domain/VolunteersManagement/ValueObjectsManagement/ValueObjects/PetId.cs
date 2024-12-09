using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

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

    public static Result<PetId, Error> Create(Guid input)
    {
        if (GuidValidator.IsEmpty(input))
            return ErrorsGeneral.ValueIsInvalid();

        return new PetId(input);
    }
}