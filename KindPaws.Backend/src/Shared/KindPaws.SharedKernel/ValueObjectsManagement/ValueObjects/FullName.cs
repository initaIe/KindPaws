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
            return ErrorsGeneral.ValueIsRequired(nameof(firstName));

        firstName = firstName.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                firstName,
                FullNameConstraints.MinFirstNameLength,
                FullNameConstraints.MaxFirstNameLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(firstName));

        if (!StringValidator.IsAlphabetic(firstName))
            return ErrorsGeneral.ValueCharacterSetIsInvalid(nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            return ErrorsGeneral.ValueIsRequired(nameof(lastName));

        lastName = lastName.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                lastName,
                FullNameConstraints.MinLastNameLength,
                FullNameConstraints.MaxLastNameLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(lastName));

        if (!StringValidator.IsAlphabetic(lastName))
            return ErrorsGeneral.ValueIsInvalid(nameof(lastName));

        if (!string.IsNullOrWhiteSpace(patronymic))
        {
            patronymic = patronymic.Trim().ToProperCase();

            if (!StringValidator.IsInRange(patronymic,
                    FullNameConstraints.MinPatronymicLength,
                    FullNameConstraints.MaxPatronymicLength))
                return ErrorsGeneral.ValueOutOfRange(nameof(firstName));

            if (!StringValidator.IsAlphabetic(patronymic))
                return ErrorsGeneral.ValueCharacterSetIsInvalid(nameof(patronymic));
        }

        return new FullName(firstName, lastName, patronymic);
    }
}