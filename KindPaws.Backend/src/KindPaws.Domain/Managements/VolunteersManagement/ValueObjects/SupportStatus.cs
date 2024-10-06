using KindPaws.Domain.Shared.Others;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

public record SupportStatus
{
    public static readonly SupportStatus NeedSupport = new(nameof(NeedSupport));
    public static readonly SupportStatus LookingHome = new(nameof(LookingHome));
    public static readonly SupportStatus AlreadyFoundHome = new(nameof(AlreadyFoundHome));

    private static readonly SupportStatus[] All = [NeedSupport, LookingHome, AlreadyFoundHome];

    private SupportStatus(string? value)
    {
        Value = value;
    }

    public string? Value { get; }

    public static Result<SupportStatus, Error> Create(string input)
    {
        input = input.Trim();

        if (All.Any(supportStatus => supportStatus.Value!.ToUpper() == input) == false)
            return Errors.General.ValueIsInvalid(input);

        return new SupportStatus(input);
    }
}