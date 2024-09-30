using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects.Constraints;

namespace KindPaws.Domain.Shared.ValueObjects;

public record FullName
{
    private FullName(
        string firstName,
        string lastName,
        string? patronymic)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string? Patronymic { get; }

    public static Result<FullName, IEnumerable<string>> Create(
        string firstName,
        string lastName,
        string? patronymic)
    {
        List<string> errors = [];

        firstName.DefaultValidate(
                FullNameConstraints.MinFirstNameLength,
                FullNameConstraints.MaxFirstNameLength)
            .AddErrorIfFailure(errors);

        lastName.DefaultValidate(
                FullNameConstraints.MinLastNameLength,
                FullNameConstraints.MaxLastNameLength)
            .AddErrorIfFailure(errors);

        patronymic?.MinMaxLengthValidate(
                FullNameConstraints.MinPatronymicLength,
                FullNameConstraints.MaxPatronymicLength)
            .AddErrorIfFailure(errors);

        patronymic?.WhiteSpacesValidate()
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new FullName(
            firstName,
            lastName,
            patronymic);
    }
}