using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects.Constraints;

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
        if (string.IsNullOrWhiteSpace(country))
            return Errors.General.ValueIsInvalid(nameof(country));

        if (!StringValidator.IsInRange(
                country,
                AddressConstraints.MinCountryLength,
                AddressConstraints.MaxCountryLength))
            return Errors.General.ValueWrongLength(nameof(country));


        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsInvalid(nameof(city));

        if (!StringValidator.IsInRange(
                city,
                AddressConstraints.MinCityLength,
                AddressConstraints.MaxCityLength))
            return Errors.General.ValueWrongLength(nameof(city));


        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsInvalid(nameof(street));

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