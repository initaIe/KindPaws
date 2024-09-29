using KindPaws.Domain.Shared.Constraints;

namespace KindPaws.Domain.Shared.ValueObjects.Constraints;

public static class FullNameConstraints
{
    public const int MinFirstNameLength = MinLengthConstraints.One;
    public const int MaxFirstNameLength = MaxLengthConstraints.Medium;

    public const int MinLastNameLength = MinLengthConstraints.One;
    public const int MaxLastNameLength = MaxLengthConstraints.Medium;

    public const int MinPatronymicLength = MinLengthConstraints.One;
    public const int MaxPatronymicLength = MaxLengthConstraints.Medium;
}