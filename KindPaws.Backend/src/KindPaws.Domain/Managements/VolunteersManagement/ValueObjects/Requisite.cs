using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record Requisite
{
    private Requisite(
        string name,
        string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }
    public string Description { get; }

    public static Result<Requisite, Error> Create(
        string name,
        string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid(nameof(name));

        if (!StringValidator.IsInRange(
                name,
                RequisiteConstraints.MinNameLength,
                RequisiteConstraints.MaxNameLength))
            return Errors.General.ValueWrongLength(nameof(name));

        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsInvalid(nameof(description));

        if (!StringValidator.IsInRange(
                description,
                RequisiteConstraints.MinDescriptionLength,
                RequisiteConstraints.MaxDescriptionLength))
            return Errors.General.ValueWrongLength(nameof(description));

        return new Requisite(name, description);
    }
}