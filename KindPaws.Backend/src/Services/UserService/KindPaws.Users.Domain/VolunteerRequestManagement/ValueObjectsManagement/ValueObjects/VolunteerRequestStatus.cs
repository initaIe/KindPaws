using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;

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
            return ErrorsGeneral.ValueIsRequired(nameof(VolunteerRequestStatus));

        if (!All.Any(h => string.Equals(h.Value, input, StringComparison.CurrentCultureIgnoreCase)))
            return ErrorsGeneral.ValueIsInvalid(nameof(VolunteerRequestStatus));

        return new VolunteerRequestStatus(input);
    }
}