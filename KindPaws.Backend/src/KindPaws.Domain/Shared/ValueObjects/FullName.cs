using KindPaws.Domain.Shared.Constraints.ValueObjectsConstraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

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

    public static Result<FullName, Error> Create(
        string firstName,
        string lastName,
        string? patronymic)
    {
        firstName = firstName.Trim();

        if (!StringValidator.IsInRange(
                firstName,
                FullNameConstraints.MinFirstNameLength,
                FullNameConstraints.MaxFirstNameLength))
            return Errors.General.ValueWrongLength(nameof(firstName));

        lastName = lastName.Trim();

        if (!StringValidator.IsInRange(
                lastName,
                FullNameConstraints.MinLastNameLength,
                FullNameConstraints.MaxLastNameLength))
            return Errors.General.ValueWrongLength(nameof(lastName));

        if (!string.IsNullOrWhiteSpace(patronymic))
        {
            patronymic = patronymic.Trim();

            if (!StringValidator.IsInRange(patronymic,
                    FullNameConstraints.MinFirstNameLength,
                    FullNameConstraints.MaxFirstNameLength))
                return Errors.General.ValueWrongLength(nameof(firstName));
        }

        return new FullName(firstName, lastName, patronymic);
    }
}