using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Species.Domain.ValueObjectsManagement.ValueObjectsConstraints;

public static class SpecieDescriptionConstraints
{
    public const int MinLength = LengthConstraints.Min.One;
    public const int MaxLength = LengthConstraints.Max.Extreme;
}