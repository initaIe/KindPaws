using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Users.Domain.VolunteerRequestManagement.ValueObjectsManagement.ValueObjects;

public record VolunteerRequestStatus
{
    public static readonly VolunteerRequestStatus Undefined = new(nameof(Undefined));
    public static readonly VolunteerRequestStatus Rejected = new(nameof(Rejected));
    public static readonly VolunteerRequestStatus Approved = new(nameof(Approved));

    private static readonly VolunteerRequestStatus[] All = [Undefined, Rejected, Approved];

    private VolunteerRequestStatus(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<VolunteerRequestStatus, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return GeneralErrors.ValueIsRequired(nameof(VolunteerRequestStatus));

        if (!All.Any(h => string.Equals(h.Value, input, StringComparison.CurrentCultureIgnoreCase)))
            return GeneralErrors.ValueIsInvalid(nameof(VolunteerRequestStatus));

        return new VolunteerRequestStatus(input);
    }
}