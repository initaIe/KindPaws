using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Shared.Constraints.VOsConstraints;

public class GenderConstraints
{
    public const int MinGenderLength = MinLengthConstraints.One;
    public const int MaxGenderLength = MaxLengthConstraints.ExtraShort;
}