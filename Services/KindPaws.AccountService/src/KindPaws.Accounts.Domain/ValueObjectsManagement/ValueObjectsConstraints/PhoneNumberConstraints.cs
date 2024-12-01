using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjectsConstraints;

public class PhoneNumberConstraints
{
    public const int MinLength = LengthConstraints.Min.Six;
    public const int MaxLength = LengthConstraints.Max.Fifteen;
}