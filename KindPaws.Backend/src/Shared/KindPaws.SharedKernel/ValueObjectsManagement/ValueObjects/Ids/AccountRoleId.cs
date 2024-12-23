using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record AccountRoleId
{
    private AccountRoleId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static AccountRoleId CreateRandom()
    {
        return new AccountRoleId(Guid.NewGuid());
    }

    public static Result<AccountRoleId, Error> Create(Guid input)
    {
        if (GuidValidator.IsEmpty(input))
            return ErrorsGeneral.ValueIsInvalid();

        return new AccountRoleId(input);
    }
}