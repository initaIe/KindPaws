using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Permissions.Domain.ValueObjectsManagement.ValueObjectsConstraints;

public class PermissionCodeConstraints
{
    public const int MinLength = LengthConstraints.Min.One;
    public const int MaxLength = LengthConstraints.Max.Medium;
}