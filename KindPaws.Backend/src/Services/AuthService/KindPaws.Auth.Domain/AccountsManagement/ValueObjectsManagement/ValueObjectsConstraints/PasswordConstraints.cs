using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Auth.Domain.AccountsManagement.ValueObjectsManagement.ValueObjectsConstraints;

public class PasswordConstraints
{
    public const int MinLength = LengthConstraints.Min.One;
    public const int MaxLength = LengthConstraints.Max.Short;
}