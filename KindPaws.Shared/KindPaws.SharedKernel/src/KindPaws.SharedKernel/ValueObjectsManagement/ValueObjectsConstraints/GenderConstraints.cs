using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

public class GenderConstraints
{
    public const int MinGenderLength = LengthConstraints.Min.One;
    public const int MaxGenderLength = LengthConstraints.Max.ExtraShort;
}