using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
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
            return Errors.General.ValueIsInvalid();

        return new AccountRoleId(input);
    }

    public static implicit operator Guid(AccountRoleId accountRoleId)
    {
        return accountRoleId?.Value
               ?? throw new ArgumentNullException($"{nameof(accountRoleId)} value cannot be null.");
    }
}