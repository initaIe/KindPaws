namespace KindPaws.Domain.Managements.VolunteerManagement.ValueObjects;

public record SupportStatus
{
    public static readonly SupportStatus NeedSupport = new(nameof(NeedSupport));
    public static readonly SupportStatus LookingHome = new(nameof(LookingHome));
    public static readonly SupportStatus AlreadyFoundHome = new(nameof(AlreadyFoundHome));

    public SupportStatus()
    {
    }

    private SupportStatus(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static SupportStatus Create(SupportStatus supportStatus)
    {
        return new SupportStatus(supportStatus.Value);
    }
}