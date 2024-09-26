using KindPaws.Domain.Managements.PetManagement.VOs.ValidationRules;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.PetManagement.VOs;

public class BreedColor
{
    private BreedColor(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public static Result<BreedColor, IEnumerable<string>> Create(string name)
    {
        List<string> errors = [];

        name.DefaultValidate(
                BreedColorRules.MinNameLength,
                BreedColorRules.MaxNameLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        var breed = new BreedColor(name);

        return breed;
    }
}