using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record Gender
{
    public static readonly Gender Undefined = new(nameof(Undefined));
    public static readonly Gender Male = new(nameof(Male));
    public static readonly Gender Female = new(nameof(Female));

    private static readonly Gender[] All = [Undefined, Male, Female];

    private Gender(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Gender, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return GeneralErrors.ValueIsRequired(nameof(Gender));

        if (!All.Any(g => string.Equals(g.Value, input, StringComparison.CurrentCultureIgnoreCase)))
            return GeneralErrors.ValueIsInvalid(nameof(Gender));

        return new Gender(input);
    }
}