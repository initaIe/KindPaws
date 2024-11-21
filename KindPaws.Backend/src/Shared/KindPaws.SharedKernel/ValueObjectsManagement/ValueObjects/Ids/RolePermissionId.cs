using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;

public record RolePermissionId
{
    private RolePermissionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static RolePermissionId CreateRandom()
    {
        return new RolePermissionId(Guid.NewGuid());
    }

    public static Result<RolePermissionId, Error> Create(Guid input)
    {
        if (GuidValidator.IsEmpty(input))
            return Errors.General.ValueIsInvalid();

        return new RolePermissionId(input);
    }

    public static implicit operator Guid(RolePermissionId rolePermissionId)
    {
        return rolePermissionId?.Value
               ?? throw new ArgumentNullException($"{nameof(rolePermissionId)} cannot be null.");
    }
}