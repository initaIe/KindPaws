using System.Text.Json.Serialization;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

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
        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsRequired(nameof(City));

        city = city.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                city,
                AddressConstraints.MinCityLength,
                AddressConstraints.MaxCityLength))
            return Errors.General.ValueOutOfRange(nameof(City));

        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsRequired(nameof(Street));

        street = street.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                street,
                AddressConstraints.MinStreetLength,
                AddressConstraints.MaxStreetLength))
            return Errors.General.ValueOutOfRange(nameof(Street));

        return new Address(city, street);
    }
}