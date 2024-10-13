using System.Text.Json.Serialization;
using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validation.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record Requisite
{
    [JsonConstructor]
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
            return Errors.General.ValueIsRequired(nameof(Name));

        name = name.Trim();

        if (!StringValidator.IsInRange(
                name,
                RequisiteConstraints.MinNameLength,
                RequisiteConstraints.MaxNameLength))
            return Errors.General.ValueOutOfRange(nameof(name));

        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsRequired(nameof(Description));

        description = description.Trim();

        if (!StringValidator.IsInRange(
                description,
                RequisiteConstraints.MinDescriptionLength,
                RequisiteConstraints.MaxDescriptionLength))
            return Errors.General.ValueOutOfRange(nameof(description));

        return new Requisite(name, description);
    }
}