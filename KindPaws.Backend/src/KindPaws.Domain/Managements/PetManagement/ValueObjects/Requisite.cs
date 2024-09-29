using KindPaws.Domain.Managements.PetManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.PetManagement.ValueObjects;

public record Requisite
{
    public Requisite()
    {
    }

    private Requisite(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }
    public string Description { get; init; }

    public static Result<Requisite, IEnumerable<string>> Create(string name, string description)
    {
        List<string> errors = [];

        name.DefaultValidate(
                RequisiteConstraints.MinNameLength,
                RequisiteConstraints.MaxNameLength)
            .AddErrorIfFailure(errors);

        description.DefaultValidate(
                RequisiteConstraints.MinDescriptionLength,
                RequisiteConstraints.MaxDescriptionLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new Requisite(name, description);
    }
}