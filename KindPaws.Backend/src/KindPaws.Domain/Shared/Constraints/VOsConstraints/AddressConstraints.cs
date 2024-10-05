using KindPaws.Domain.Shared.Constraints.BaseConstraints;

namespace KindPaws.Domain.Shared.Constraints.VOsConstraints;

public static class AddressConstraints
{
    public const int MinCountryLength = MinLengthConstraints.One;
    public const int MaxCountryLength = MaxLengthConstraints.Medium;

    public const int MinCityLength = MinLengthConstraints.One;
    public const int MaxCityLength = MaxLengthConstraints.Medium;

    public const int MinStreetLength = MinLengthConstraints.One;
    public const int MaxStreetLength = MaxLengthConstraints.Medium;
}