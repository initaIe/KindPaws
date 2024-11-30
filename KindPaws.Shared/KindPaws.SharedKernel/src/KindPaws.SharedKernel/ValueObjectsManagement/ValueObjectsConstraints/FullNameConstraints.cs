using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

public static class FullNameConstraints
{
    public const int MinFirstNameLength = LengthConstraints.Min.One;
    public const int MaxFirstNameLength = LengthConstraints.Max.Medium;

    public const int MinLastNameLength = LengthConstraints.Min.One;
    public const int MaxLastNameLength = LengthConstraints.Max.Medium;

    public const int MinPatronymicLength = LengthConstraints.Min.One;
    public const int MaxPatronymicLength = LengthConstraints.Max.Medium;
}