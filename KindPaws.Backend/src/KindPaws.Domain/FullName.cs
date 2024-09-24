using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.ValidationRules;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class FullName
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

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? Patronymic { get; private set; }

    public static Result<FullName, IEnumerable<string>> Create(
        string firstName,
        string lastName,
        string? patronymic)
    {
        List<string> errors = [];

        firstName.DefaultValidate(
                FullNameRules.MinNameLength,
                FullNameRules.MaxNameLength)
            .AddErrorIfFailure(errors);

        lastName.DefaultValidate(
                FullNameRules.MinLastNameLength,
                FullNameRules.MaxLastNameLength)
            .AddErrorIfFailure(errors);


        patronymic.DefaultValidate(
                FullNameRules.MinPatronymicLength,
                FullNameRules.MaxPatronymicLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return Result.Failure<FullName, IEnumerable<string>>(errors);

        var fullName = new FullName(
            firstName,
            lastName,
            patronymic);

        return Result.Success<FullName, IEnumerable<string>>(fullName);
    }
}