using System.Text.Json.Serialization;
using KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Shared.ValueObjects;

public record Address
{
    [JsonConstructor]
    private Address(
        string city,
        string street)
    {
        City = city;
        Street = street;
    }

    public string City { get; }
    public string Street { get; }

    public static Result<Address, Error> Create(
        string city,
        string street)
    {
        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsRequired(nameof(City));

        city = city.Trim();

        if (!StringValidator.IsInRange(
                city,
                AddressConstraints.MinCityLength,
                AddressConstraints.MaxCityLength))
            return Errors.General.ValueOutOfRange(nameof(City));

        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsRequired(nameof(Street));

        street = street.Trim();

        if (!StringValidator.IsInRange(
                street,
                AddressConstraints.MinStreetLength,
                AddressConstraints.MaxStreetLength))
            return Errors.General.ValueOutOfRange(nameof(Street));

        return new Address(city, street);
    }
}