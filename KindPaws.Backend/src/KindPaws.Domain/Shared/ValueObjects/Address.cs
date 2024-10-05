using KindPaws.Domain.Shared.Constraints.VOsConstraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Shared.ValueObjects;

public record Address
{
    private Address(
        string? country,
        string? city,
        string? street)
    {
        Country = country;
        City = city;
        Street = street;
    }

    public string? Country { get; }
    public string? City { get; }
    public string? Street { get; }

    public static Result<Address, Error> Create(
        string country,
        string city,
        string street)
    {
        country = country.Trim();

        if (!StringValidator.IsInRange(
                country,
                AddressConstraints.MinCountryLength,
                AddressConstraints.MaxCountryLength))
            return Errors.General.ValueWrongLength(nameof(country));

        city = city.Trim();

        if (!StringValidator.IsInRange(
                city,
                AddressConstraints.MinCityLength,
                AddressConstraints.MaxCityLength))
            return Errors.General.ValueWrongLength(nameof(city));

        street = street.Trim();

        if (!StringValidator.IsInRange(
                street,
                AddressConstraints.MinStreetLength,
                AddressConstraints.MaxStreetLength))
            return Errors.General.ValueWrongLength(nameof(street));

        return new Address(country, city, street);
    }

    public static Address CreateEmpty()
    {
        return new Address(null, null, null);
    }
}