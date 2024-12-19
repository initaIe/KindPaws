using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjectsConstraints;

public class PasswordHashConstraints
{
    public const int MinLength = LengthConstraints.Min.Long;
    public const int MaxLength = LengthConstraints.Max.ExtraLong;
}