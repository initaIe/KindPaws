using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;

public static class UserDescriptionConstraints
{
    public const int MinLength = LengthConstraints.Min.Short;
    public const int MaxLength = LengthConstraints.Max.Huge;
}