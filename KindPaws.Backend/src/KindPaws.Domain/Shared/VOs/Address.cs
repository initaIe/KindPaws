using KindPaws.Domain.Shared.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Shared.VOs;

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
                MinLengthConstraints.MinLengthOne, 
                MaxLengthConstraints.MaxLengthSmall)
            .AddErrorIfFailure(errors);

        city.DefaultValidate(
                MinLengthConstraints.MinLengthOne, 
                MaxLengthConstraints.MaxLengthSmall)
            .AddErrorIfFailure(errors);

        street.DefaultValidate(
                MinLengthConstraints.MinLengthOne, 
                MaxLengthConstraints.MaxLengthSmall)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        var address = new Address(
            country,
            city,
            street);

        return address;
    }
}