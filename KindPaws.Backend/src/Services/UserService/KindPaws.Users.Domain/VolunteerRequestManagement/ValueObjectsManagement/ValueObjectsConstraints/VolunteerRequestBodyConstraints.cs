using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Users.Domain.VolunteerRequestManagement.ValueObjectsManagement.ValueObjectsConstraints;

public static class VolunteerRequestBodyConstraints
{
    public const int MinLength = LengthConstraints.Min.Long;
    public const int MaxLength = LengthConstraints.Max.Huge;
}