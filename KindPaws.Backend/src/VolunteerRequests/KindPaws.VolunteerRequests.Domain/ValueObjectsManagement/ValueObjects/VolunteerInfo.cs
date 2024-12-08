using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjects;

// TODO: добавить сертификаты и тд..
public record VolunteerInfo
{
    private VolunteerInfo(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<VolunteerInfo, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return GeneralErrors.General.ValueIsRequired(nameof(VolunteerInfo));

        if (!StringValidator.IsInRange(
                input,
                VolunteerInfoConstraints.MinLength,
                VolunteerInfoConstraints.MaxLength))
            return GeneralErrors.General.ValueOutOfRange(nameof(VolunteerInfo));

        return new VolunteerInfo(input);
    }
}