using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.Utilities.ValidationManagement.Validators;

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

    public static VolunteerId CreateEmpty()
    {
        return new VolunteerId(Guid.Empty);
    }

    public static Result<VolunteerId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid();

        return new VolunteerId(value);
    }

    public static implicit operator Guid(VolunteerId volunteerId)
    {
        return volunteerId?.Value
               ?? throw new ArgumentNullException($"{nameof(volunteerId)} value cannot be null.");
    }
}