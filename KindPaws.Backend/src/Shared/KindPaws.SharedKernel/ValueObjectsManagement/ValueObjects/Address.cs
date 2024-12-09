using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record Address
{
    private Address(
        string country,
        string city,
        string street)
    {
        Country = country;
        City = city;
        Street = street;
    }

    public string Country { get; }
    public string City { get; }
    public string Street { get; }

    public static Result<Address, Error> Create(
        string country,
        string city,
        string street)
    {
        if (string.IsNullOrWhiteSpace(country))
            return ErrorsGeneral.ValueIsRequired(nameof(country));

        country = country.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                country,
                AddressConstraints.MinCityLength,
                AddressConstraints.MaxCityLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(country));

        if (string.IsNullOrWhiteSpace(city))
            return ErrorsGeneral.ValueIsRequired(nameof(City));

        city = city.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                city,
                AddressConstraints.MinCityLength,
                AddressConstraints.MaxCityLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(City));

        if (string.IsNullOrWhiteSpace(street))
            return ErrorsGeneral.ValueIsRequired(nameof(Street));

        street = street.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                street,
                AddressConstraints.MinStreetLength,
                AddressConstraints.MaxStreetLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(Street));

        return new Address(
            country,
            city,
            street);
    }
}