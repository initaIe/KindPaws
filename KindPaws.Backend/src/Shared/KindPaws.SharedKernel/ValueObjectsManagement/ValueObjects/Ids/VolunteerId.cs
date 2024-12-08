using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record VolunteerId
{
    private VolunteerId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static VolunteerId CreateRandom()
    {
        return new VolunteerId(Guid.NewGuid());
    }

    public static Result<VolunteerId, Error> Create(Guid input)
    {
        if (GuidValidator.IsEmpty(input))
            return GeneralErrors.General.ValueIsInvalid();

        return new VolunteerId(input);
    }
}