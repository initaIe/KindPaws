using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Auth.Domain.PermissionsManagement.ValueObjectsManagement.ValueObjectsConstraints;

public class PermissionCodeConstraints
{
    public const int MinLength = LengthConstraints.Min.Eight;
    public const int MaxLength = LengthConstraints.Max.Medium;
}