using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

public static class UsernameConstraints
{
    public const int MinLength = LengthConstraints.Min.Three;
    public const int MaxLength = LengthConstraints.Max.ExtraShort;
}