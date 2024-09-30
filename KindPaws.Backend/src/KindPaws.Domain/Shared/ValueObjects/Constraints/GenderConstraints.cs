using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Shared.ValueObjects.Constraints;

public class GenderConstraints
{
    public const int MinGenderLength = MinLengthConstraints.One;
    public const int MaxGenderLength = MaxLengthConstraints.ExtraShort;
}