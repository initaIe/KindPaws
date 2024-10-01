using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Managements.VolunteersManagement.Constraints;

public class SupportStatusConstraints
{
    public const int MinLength = MinLengthConstraints.One;
    public const int MaxLength = MaxLengthConstraints.Medium;
}