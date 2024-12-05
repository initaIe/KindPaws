using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record RefreshSessionId
{
    private RefreshSessionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static RefreshSessionId CreateRandom()
    {
        return new RefreshSessionId(Guid.NewGuid());
    }

    public static Result<RefreshSessionId, Error> Create(Guid input)
    {
        if (GuidValidator.IsEmpty(input))
            return Errors.General.ValueIsInvalid();

        return new RefreshSessionId(input);
    }
}