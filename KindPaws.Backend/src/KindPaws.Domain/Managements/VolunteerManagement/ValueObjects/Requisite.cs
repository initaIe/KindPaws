using KindPaws.Domain.Managements.VolunteerManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.VolunteerManagement.ValueObjects;

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

    public string Name { get; }
    public string Description { get; }

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