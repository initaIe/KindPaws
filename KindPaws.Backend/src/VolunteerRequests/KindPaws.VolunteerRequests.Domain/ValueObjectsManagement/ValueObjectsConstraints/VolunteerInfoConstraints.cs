using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjectsConstraints;

public static class VolunteerInfoConstraints
{
    public const int MinLength = LengthConstraints.Min.Long;
    public const int MaxLength = LengthConstraints.Max.Huge;
}