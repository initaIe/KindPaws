using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;

public static class VolunteerDescriptionConstraints
{
    public const int MinLength = LengthConstraints.Min.One;
    public const int MaxLength = LengthConstraints.Max.Extreme;
}