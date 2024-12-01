using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjects;

public record VolunteerRequestStatus
{
    public static readonly VolunteerRequestStatus Submitted = new(nameof(Submitted));
    public static readonly VolunteerRequestStatus Rejected = new(nameof(Rejected));
    public static readonly VolunteerRequestStatus RevisionRequired = new(nameof(RevisionRequired));
    public static readonly VolunteerRequestStatus Approved = new(nameof(Approved));

    private static readonly VolunteerRequestStatus[] All = [Submitted, Rejected, RevisionRequired, Approved];

    private VolunteerRequestStatus(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<VolunteerRequestStatus, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(VolunteerRequestStatus));

        if (!All.Any(h => string.Equals(h.Value, input, StringComparison.CurrentCultureIgnoreCase)))
            return Errors.General.ValueIsInvalid(nameof(VolunteerRequestStatus));

        return new VolunteerRequestStatus(input);
    }
}