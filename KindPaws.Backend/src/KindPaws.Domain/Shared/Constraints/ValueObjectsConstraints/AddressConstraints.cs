using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;

public static class AddressConstraints
{
    public const int MinCityLength = LengthConstraints.Min.One;
    public const int MaxCityLength = LengthConstraints.Max.Medium;

    public const int MinStreetLength = LengthConstraints.Min.One;
    public const int MaxStreetLength = LengthConstraints.Max.Medium;
}