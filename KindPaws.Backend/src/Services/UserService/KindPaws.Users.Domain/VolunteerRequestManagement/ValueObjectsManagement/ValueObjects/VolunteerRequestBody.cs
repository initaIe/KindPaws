using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Users.Domain.VolunteerRequestManagement.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Users.Domain.VolunteerRequestManagement.ValueObjectsManagement.ValueObjects;

// TODO: ADD FILES/PHOTOS/CERTIFICATES AND ETC..
public record VolunteerRequestBody
{
    private VolunteerRequestBody(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<VolunteerRequestBody, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return ErrorsGeneral.ValueIsRequired(nameof(VolunteerRequestBody));

        if (!StringValidator.IsInRange(
                input,
                VolunteerRequestBodyConstraints.MinLength,
                VolunteerRequestBodyConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(VolunteerRequestBody));

        return new VolunteerRequestBody(input);
    }
}