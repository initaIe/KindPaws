using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

public record VolunteerDescription
{
    private VolunteerDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<VolunteerDescription, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return GeneralErrors.General.ValueIsRequired(nameof(VolunteerDescription));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                VolunteerDescriptionConstraints.MinLength,
                VolunteerDescriptionConstraints.MaxLength))
            return GeneralErrors.General.ValueOutOfRange(nameof(input));

        return new VolunteerDescription(input);
    }
}