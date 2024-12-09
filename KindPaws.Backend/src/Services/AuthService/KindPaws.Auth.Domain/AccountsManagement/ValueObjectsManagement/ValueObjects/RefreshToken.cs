using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjects;

public record RefreshToken
{
    private RefreshToken(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static RefreshToken CreateRandom()
    {
        return new RefreshToken(Guid.NewGuid());
    }

    public static Result<RefreshToken, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return ErrorsGeneral.ValueIsInvalid(nameof(RefreshToken));

        return new RefreshToken(value);
    }
}