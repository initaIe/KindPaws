using KindPaws.Domain.Managements.SpecieManagement.Constraints;
using KindPaws.Domain.Managements.SpecieManagement.ValueObjects.Lists;
using KindPaws.Domain.Shared.IDs;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.ValueObjects;

namespace KindPaws.Domain.Managements.SpecieManagement.Entities;

public class Breed : Entity<BreedId>
{
    public Breed(BreedId id) : base(id)
    {
    }

    public Breed(
        BreedId id,
        string name,
        string description,
        BreedColorList colorList)
        : base(id)
    {
        Name = name;
        Description = description;
        ColorList = colorList;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public BreedColorList ColorList { get; private set; }

    public static Result<Breed, IEnumerable<string>> Create(
        BreedId id,
        string name,
        string description,
        BreedColorList colorList)
    {
        List<string> errors = [];

        name.DefaultValidate(
                BreedConstraints.MinNameLength,
                BreedConstraints.MaxNameLength)
            .AddErrorIfFailure(errors);

        description.DefaultValidate(
                BreedConstraints.MinDescriptionLength,
                BreedConstraints.MaxDescriptionLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new Breed(
            id,
            name,
            description,
            colorList);
    }
}