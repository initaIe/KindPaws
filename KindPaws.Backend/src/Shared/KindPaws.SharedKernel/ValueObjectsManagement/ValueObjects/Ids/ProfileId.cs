using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record ProfileId
{
    private ProfileId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ProfileId CreateRandom()
    {
        return new ProfileId(Guid.NewGuid());
    }

    public static Result<ProfileId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid();

        return new ProfileId(value);
    }
}