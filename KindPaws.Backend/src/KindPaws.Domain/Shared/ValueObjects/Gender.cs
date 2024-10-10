using KindPaws.Domain.Shared.Others;

namespace KindPaws.Domain.Shared.ValueObjects;

public record Gender
{
    public static readonly Gender Male = new(nameof(Male));
    public static readonly Gender Female = new(nameof(Female));

    private static readonly Gender[] All = [Male, Female];

    private Gender(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Gender, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Errors.General.ValueIsRequired(nameof(Gender));
        
        if (!All.All(gender => 
                string.Equals(gender.Value!, input, StringComparison.CurrentCultureIgnoreCase)))
            return Errors.General.ValueIsInvalid(nameof(Gender));

        return new Gender(input);
    }
}