using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjectsConstraints;

public static class UserNameConstraints
{
    public const int MinLength = LengthConstraints.Min.Three;
    public const int MaxLength = LengthConstraints.Max.ExtraShort;
}