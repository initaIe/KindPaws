using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

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
            return GeneralErrors.ValueIsRequired(nameof(VolunteerDescription));

        input = input.Trim();

        if (!StringValidator.IsInRange(
                input,
                VolunteerDescriptionConstraints.MinLength,
                VolunteerDescriptionConstraints.MaxLength))
            return GeneralErrors.ValueOutOfRange(nameof(input));

        return new VolunteerDescription(input);
    }
}