using KindPaws.Domain.Managements.PetManagement.VOs.ValidationRules;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.PetManagement.VOs;

public class PetBreed
{
    private readonly List<BreedColor> _breedColors;

    private PetBreed(
        string name,
        string description,
        List<BreedColor> breedColors)
    {
        Name = name;
        Description = description;
        _breedColors = breedColors;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyList<BreedColor> BreedColors => _breedColors;

    public static Result<PetBreed, IEnumerable<string>> Create(
        List<BreedColor> breedColors,
        string name,
        string description)
    {
        List<string> errors = [];

        name.DefaultValidate(
                BreedRules.MinNameLength,
                BreedRules.MaxNameLength)
            .AddErrorIfFailure(errors);

        description.DefaultValidate(
                BreedRules.MinDescriptionLength,
                BreedRules.MaxDescriptionLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        var breed = new PetBreed(name, description, breedColors);

        return breed;
    }
}