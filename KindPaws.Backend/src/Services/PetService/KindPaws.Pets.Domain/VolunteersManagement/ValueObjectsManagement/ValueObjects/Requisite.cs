using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

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
            return GeneralErrors.General.ValueIsRequired(nameof(Name));

        name = name.Trim().ToProperCase();

        if (!StringValidator.IsInRange(
                name,
                RequisiteConstraints.MinNameLength,
                RequisiteConstraints.MaxNameLength))
            return GeneralErrors.General.ValueOutOfRange(nameof(name));

        if (string.IsNullOrWhiteSpace(description))
            return GeneralErrors.General.ValueIsRequired(nameof(Description));

        description = description.Trim();

        if (!StringValidator.IsInRange(
                description,
                RequisiteConstraints.MinDescriptionLength,
                RequisiteConstraints.MaxDescriptionLength))
            return GeneralErrors.General.ValueOutOfRange(nameof(description));

        return new Requisite(name, description);
    }
}