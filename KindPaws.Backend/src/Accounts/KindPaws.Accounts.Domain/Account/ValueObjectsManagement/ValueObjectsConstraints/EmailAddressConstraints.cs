using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Accounts.Domain.Account.ValueObjectsManagement.ValueObjectsConstraints;

public class EmailAddressConstraints
{
    public const int MinLength = LengthConstraints.Min.Five;
    public const int MaxLength = LengthConstraints.Max.Long;
}