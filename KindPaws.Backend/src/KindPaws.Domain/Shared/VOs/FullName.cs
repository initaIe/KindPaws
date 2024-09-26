using KindPaws.Domain.Shared.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Shared.VOs;

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
                MinLengthConstraints.MinLengthOne, 
                MaxLengthConstraints.MaxLengthSmall)
            .AddErrorIfFailure(errors);

        lastName.DefaultValidate(
                MinLengthConstraints.MinLengthOne, 
                MaxLengthConstraints.MaxLengthSmall)
            .AddErrorIfFailure(errors);


        patronymic.DefaultValidate(
                MinLengthConstraints.MinLengthOne, 
                MaxLengthConstraints.MaxLengthSmall)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        var fullName = new FullName(
            firstName,
            lastName,
            patronymic);

        return fullName;
    }
}