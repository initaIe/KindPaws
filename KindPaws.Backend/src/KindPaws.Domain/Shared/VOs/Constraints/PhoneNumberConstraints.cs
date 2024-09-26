using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Shared.VOs.Constraints;

public class PhoneNumberConstraints
{
    public const int MinLength = MinLengthConstraints.MinLengthOne;
    public const int MaxLength = MaxLengthConstraints.MaxLengthExtraSmall;
}