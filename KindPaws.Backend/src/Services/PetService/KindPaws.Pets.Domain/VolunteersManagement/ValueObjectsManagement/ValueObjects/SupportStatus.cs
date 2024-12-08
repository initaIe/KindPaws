using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Extensions;

namespace KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

public record SupportStatus
{
    public static readonly SupportStatus Undefined = new(nameof(Undefined));
    public static readonly SupportStatus NeedSupport = new(nameof(NeedSupport));
    public static readonly SupportStatus LookingHome = new(nameof(LookingHome));
    public static readonly SupportStatus AlreadyFoundHome = new(nameof(AlreadyFoundHome));

    private static readonly SupportStatus[] All = [Undefined, NeedSupport, LookingHome, AlreadyFoundHome];

    private SupportStatus(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<SupportStatus, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return GeneralErrors.General.ValueIsRequired(nameof(SupportStatus));

        input = input.Trim().ToProperCase();

        if (!All.Any(s => string.Equals(s.Value, input, StringComparison.CurrentCultureIgnoreCase)))
            return GeneralErrors.General.ValueIsInvalid(nameof(SupportStatus));

        return new SupportStatus(input);
    }
}