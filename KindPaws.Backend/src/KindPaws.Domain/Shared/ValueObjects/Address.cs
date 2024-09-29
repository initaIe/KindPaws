using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects.Constraints;

namespace KindPaws.Domain.Shared.ValueObjects;

public record Address
{
    public Address()
    {
    }

    private Address(
        string country,
        string city,
        string street)
    {
        Country = country;
        City = city;
        Street = street;
    }

    public string Country { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }

    public static Result<Address, IEnumerable<string>> Create(
        string country,
        string city,
        string street)
    {
        List<string> errors = [];

        country.DefaultValidate(
                AddressConstraints.MinCountryLength,
                AddressConstraints.MaxCountryLength)
            .AddErrorIfFailure(errors);

        city.DefaultValidate(
                AddressConstraints.MinCityLength,
                AddressConstraints.MaxCityLength)
            .AddErrorIfFailure(errors);

        street.DefaultValidate(
                AddressConstraints.MinStreetLength,
                AddressConstraints.MaxStreetLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new Address(
            country,
            city,
            street);
    }
}