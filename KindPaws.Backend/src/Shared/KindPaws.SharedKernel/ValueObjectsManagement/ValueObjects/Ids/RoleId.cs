using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record RoleId
{
    private RoleId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static RoleId CreateRandom()
    {
        return new RoleId(Guid.NewGuid());
    }

    public static Result<RoleId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid();

        return new RoleId(value);
    }

    public static implicit operator Guid(RoleId roleId)
    {
        return roleId?.Value
               ?? throw new ArgumentNullException($"{nameof(roleId)} cannot be null.");
    }
}