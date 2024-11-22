using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

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
        if (string.IsNullOrWhiteSpace(firstName))
            return Errors.General.ValueIsRequired(nameof(firstName));

        firstName = firstName.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                firstName,
                FullNameConstraints.MinFirstNameLength,
                FullNameConstraints.MaxFirstNameLength))
            return Errors.General.ValueOutOfRange(nameof(firstName));

        if (!StringValidator.IsAlphabetic(firstName))
            return Errors.General.ValueCharacterSetIsInvalid(nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            return Errors.General.ValueIsRequired(nameof(lastName));

        lastName = lastName.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                lastName,
                FullNameConstraints.MinLastNameLength,
                FullNameConstraints.MaxLastNameLength))
            return Errors.General.ValueOutOfRange(nameof(lastName));

        if (!StringValidator.IsAlphabetic(lastName))
            return Errors.General.ValueIsInvalid(nameof(lastName));

        if (!string.IsNullOrWhiteSpace(patronymic))
        {
            patronymic = patronymic.Trim().ToProperCase();

            if (!StringValidator.IsInRange(patronymic,
                    FullNameConstraints.MinPatronymicLength,
                    FullNameConstraints.MaxPatronymicLength))
                return Errors.General.ValueOutOfRange(nameof(firstName));

            if (!StringValidator.IsAlphabetic(patronymic))
                return Errors.General.ValueCharacterSetIsInvalid(nameof(patronymic));
        }

        return new FullName(firstName, lastName, patronymic);
    }
}