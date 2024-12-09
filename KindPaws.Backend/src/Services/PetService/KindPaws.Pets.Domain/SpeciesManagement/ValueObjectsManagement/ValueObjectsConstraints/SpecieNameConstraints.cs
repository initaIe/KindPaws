using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints.BaseConstraints;

namespace KindPaws.Pets.Domain.SpeciesManagement.ValueObjectsManagement.ValueObjectsConstraints;

public static class SpecieNameConstraints
{
    public const int MinLength = LengthConstraints.Min.One;
    public const int MaxLength = LengthConstraints.Max.Short;
}