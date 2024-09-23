using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class HelpDetail
{
    public const int MinNameLength = 1;
    public const int MaxNameLength = 25;
    public const int MinDescriptionLength = 1;
    public const int MaxDescriptionLength = 250;

    private HelpDetail(Guid helpInfoId, string name, string description)
    {
        HelpInfoId = helpInfoId;
        Name = name;
        Description = description;
    }

    public Guid HelpInfoId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public static Result<HelpDetail, IEnumerable<string>> Create(Guid helpInfoId, string name, string description)
    {
        List<string> errors = [];

        helpInfoId.Validate().AddErrorIfFailure(errors);
        name.DefaultValidate(MinNameLength, MaxNameLength);
        description.DefaultValidate(MinDescriptionLength, MaxDescriptionLength);

        if (errors.Count > 0) return Result.Failure<HelpDetail, IEnumerable<string>>(errors);

        var helpDetail = new HelpDetail(helpInfoId, name, description);

        return Result.Success<HelpDetail, IEnumerable<string>>(helpDetail);
    }
}