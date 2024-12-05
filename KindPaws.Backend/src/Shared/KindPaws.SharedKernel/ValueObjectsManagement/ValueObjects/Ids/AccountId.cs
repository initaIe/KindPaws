using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record AccountId
{
    private AccountId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static AccountId CreateRandom()
    {
        return new AccountId(Guid.NewGuid());
    }

    public static Result<AccountId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid();

        return new AccountId(value);
    }
}