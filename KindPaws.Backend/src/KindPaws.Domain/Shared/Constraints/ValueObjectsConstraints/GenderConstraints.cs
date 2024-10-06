using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;

public class GenderConstraints
{
    public const int MinGenderLength = LengthConstraints.Min.One;
    public const int MaxGenderLength = LengthConstraints.Max.ExtraShort;
}