using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

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
            return ErrorsGeneral.ValueIsInvalid();

        return new ProfileId(value);
    }
}