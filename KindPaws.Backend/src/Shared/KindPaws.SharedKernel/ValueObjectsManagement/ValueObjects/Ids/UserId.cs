using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record UserId
{
    private UserId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static UserId CreateRandom()
    {
        return new UserId(Guid.NewGuid());
    }

    public static Result<UserId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return ErrorsGeneral.ValueIsInvalid();

        return new UserId(value);
    }
}