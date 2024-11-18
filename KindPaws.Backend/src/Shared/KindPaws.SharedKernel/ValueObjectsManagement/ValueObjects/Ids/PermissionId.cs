using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record PermissionId
{
    private PermissionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PermissionId CreateRandom()
    {
        return new PermissionId(Guid.NewGuid());
    }

    public static PermissionId CreateEmpty()
    {
        return new PermissionId(Guid.Empty);
    }

    public static Result<PermissionId, Error> Create(Guid value)
    {
        if (GuidValidator.IsEmpty(value))
            return Errors.General.ValueIsInvalid();

        return new PermissionId(value);
    }

    public static implicit operator Guid(PermissionId permissionId)
    {
        return permissionId?.Value
               ?? throw new ArgumentNullException($"{nameof(permissionId)} cannot be null.");
    }
}