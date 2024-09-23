using CSharpFunctionalExtensions;
using KindPaws.Domain.Helpers;
using KindPaws.Domain.Validators;

namespace KindPaws.Domain;

public class BreedColor
{
    public const int MinNameLength = 1;
    public const int MaxNameLength = 25;

    private BreedColor(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static Result<BreedColor, IEnumerable<string>> Create(Guid id, string name)
    {
        List<string> errors = [];

        id.Validate().AddErrorIfFailure(errors);
        name.DefaultValidate(MinNameLength, MaxNameLength);

        if (errors.Count > 0) return Result.Failure<BreedColor, IEnumerable<string>>(errors);

        var breed = new BreedColor(id, name);

        return Result.Success<BreedColor, IEnumerable<string>>(breed);
    }
}