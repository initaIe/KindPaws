using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

public static class MediumDescriptionConstraints
{
    public const int MinLength = LengthConstraints.Min.One;
    public const int MaxLength = LengthConstraints.Max.Extreme;
}