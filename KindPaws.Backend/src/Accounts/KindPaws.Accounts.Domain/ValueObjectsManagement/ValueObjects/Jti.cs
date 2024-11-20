using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;

public class Jti
{
    private Jti(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static Jti CreateRandom()
    {
        return new Jti(Guid.Empty);
    }

    public static Result<Jti, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid(nameof(Jti));

        return new Jti(value);
    }
}