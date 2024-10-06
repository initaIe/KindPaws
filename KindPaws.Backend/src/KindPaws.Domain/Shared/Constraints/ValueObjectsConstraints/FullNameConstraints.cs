using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;

public static class FullNameConstraints
{
    public const int MinFirstNameLength = LengthConstraints.Min.One;
    public const int MaxFirstNameLength = LengthConstraints.Max.Medium;

    public const int MinLastNameLength = LengthConstraints.Min.One;
    public const int MaxLastNameLength = LengthConstraints.Max.Medium;

    public const int MinPatronymicLength = LengthConstraints.Min.One;
    public const int MaxPatronymicLength = LengthConstraints.Max.Medium;
}