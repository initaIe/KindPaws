using KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Shared.ValueObjects;

public record Address
{
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

        return new Address( city, street);
    }
}