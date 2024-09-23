using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class HelpStatus
{
    public const int MinDescriptionLength = 1;
    public const int MaxDescriptionLength = 250;

    private HelpStatus(Guid helpInfoId, string description)
    {
        HelpInfoId = helpInfoId;
        Description = description;
    }

    public Guid HelpInfoId { get; private set; }
    public string Description { get; private set; }

    public static Result<HelpStatus, IEnumerable<string>> Create(Guid helpInfoId, string description)
    {
        List<string> errors = [];

        helpInfoId.Validate().AddErrorIfFailure(errors);
        description.DefaultValidate(MinDescriptionLength, MaxDescriptionLength);

        if (errors.Count > 0) return Result.Failure<HelpStatus, IEnumerable<string>>(errors);

        var helpStatus = new HelpStatus(helpInfoId, description);

        return Result.Success<HelpStatus, IEnumerable<string>>(helpStatus);
    }
}