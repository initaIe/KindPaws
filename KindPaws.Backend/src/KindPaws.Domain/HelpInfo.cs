using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class HelpInfo
{
    private readonly List<HelpDetail> _details;

    private HelpInfo(
        Guid petId,
        List<HelpDetail> details,
        HelpStatus status)
    {
        PetId = petId;
        _details = details;
        Status = status;
    }

    public Guid PetId { get; private set; }
    public HelpStatus Status { get; private set; }
    public IReadOnlyList<HelpDetail> Details => _details;

    public static Result<HelpInfo, IEnumerable<string>> Create(
        Guid petId,
        List<HelpDetail> details,
        HelpStatus status)
    {
        List<string> errors = [];

        petId.Validate().AddErrorIfFailure(errors);

        if (errors.Count > 0) return Result.Failure<HelpInfo, IEnumerable<string>>(errors);

        var helpInfo = new HelpInfo(
            petId,
            details,
            status);

        return Result.Success<HelpInfo, IEnumerable<string>>(helpInfo);
    }
}