using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class Address
{
    private Address(
        string country,
        string city,
        string street,
        string houseNumber,
        string apartmentNumber)
    {
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        ApartmentNumber = apartmentNumber;
    }

    public string Country { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string HouseNumber { get; private set; }
    public string ApartmentNumber { get; private set; }

    public static Result<Address, IEnumerable<string>> Create(
        string country,
        string city,
        string street,
        string houseNumber,
        string apartmentNumber)
    {
        List<string> errors = [];

        country.DefaultValidate(1, 30)
            .AddErrorIfFailure(errors);

        city.DefaultValidate(1, 30)
            .AddErrorIfFailure(errors);

        street.DefaultValidate(1, 30)
            .AddErrorIfFailure(errors);

        houseNumber.DefaultValidate(1, 10)
            .AddErrorIfFailure(errors);

        apartmentNumber.DefaultValidate(1, 10)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return Result.Failure<Address, IEnumerable<string>>(errors);

        var address = new Address(
            country,
            city,
            street,
            houseNumber,
            apartmentNumber);

        return Result.Success<Address, IEnumerable<string>>(address);
    }
}