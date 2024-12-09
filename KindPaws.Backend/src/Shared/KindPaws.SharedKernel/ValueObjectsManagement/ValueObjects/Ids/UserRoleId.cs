using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record UserRoleId
{
    private UserRoleId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static UserRoleId CreateRandom()
    {
        return new UserRoleId(Guid.NewGuid());
    }

    public static Result<UserRoleId, Error> Create(Guid input)
    {
        if (GuidValidator.IsEmpty(input))
            return GeneralErrors.ValueIsInvalid();

        return new UserRoleId(input);
    }
}