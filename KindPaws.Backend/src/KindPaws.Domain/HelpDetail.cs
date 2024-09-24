using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.ValidationRules;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class HelpDetail
{
    private HelpDetail(
        string name,
        string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public static Result<HelpDetail, IEnumerable<string>> Create(
        string name,
        string description)
    {
        List<string> errors = [];

        name.DefaultValidate(
                HelpDetailRules.MinNameLength,
                HelpDetailRules.MaxNameLength)
            .AddErrorsIfFailure(errors);

        description.DefaultValidate(
                HelpDetailRules.MinDescriptionLength,
                HelpDetailRules.MaxDescriptionLength)
            .AddErrorsIfFailure(errors);

        if (errors.Count > 0) return Result.Failure<HelpDetail, IEnumerable<string>>(errors);

        var helpDetail = new HelpDetail(name, description);

        return Result.Success<HelpDetail, IEnumerable<string>>(helpDetail);
    }
}