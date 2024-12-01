using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record VolunteerRequestId
{
    private VolunteerRequestId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static VolunteerRequestId CreateRandom()
    {
        return new VolunteerRequestId(Guid.NewGuid());
    }

    public static Result<VolunteerRequestId, Error> Create(Guid input)
    {
        if (GuidValidator.IsEmpty(input))
            return Errors.General.ValueIsInvalid();

        return new VolunteerRequestId(input);
    }

    public static implicit operator Guid(VolunteerRequestId volunteerRequestId)
    {
        return volunteerRequestId?.Value
               ?? throw new ArgumentNullException($"{nameof(volunteerRequestId)} cannot be null.");
    }
}