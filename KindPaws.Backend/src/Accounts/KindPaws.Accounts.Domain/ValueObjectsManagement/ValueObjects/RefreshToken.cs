using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;

public class RefreshToken
{
    private RefreshToken(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static RefreshToken CreateRandom()
    {
        return new RefreshToken(Guid.Empty);
    }

    public static Result<RefreshToken, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid(nameof(RefreshToken));

        return new RefreshToken(value);
    }
}