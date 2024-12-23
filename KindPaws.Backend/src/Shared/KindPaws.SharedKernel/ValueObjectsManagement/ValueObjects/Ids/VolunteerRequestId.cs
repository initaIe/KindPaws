using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
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
            return ErrorsGeneral.ValueIsInvalid();

        return new VolunteerRequestId(input);
    }
}