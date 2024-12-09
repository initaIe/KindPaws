using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

public record Disease
{
    private Disease(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Disease, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return GeneralErrors.ValueIsRequired(nameof(Disease));

        input = input.Trim();

        if (!StringValidator.IsInRange(input, DiseaseConstraints.MinLength, DiseaseConstraints.MaxLength))
            return GeneralErrors.ValueOutOfRange(nameof(input));

        return new Disease(input);
    }
}