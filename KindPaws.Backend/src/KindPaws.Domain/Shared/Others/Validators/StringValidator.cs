namespace KindPaws.Domain.Shared.Others.Validators;

public static class StringValidator
{
    public static Result<string> MinMaxLengthValidate(this string input, int minLength, int maxLength)
    {
        if (input.Length < minLength)
            return $"Length must be larger than or equal {minLength}.";

        if (input.Length > maxLength)
            return $"Length must be smaller than or equal {maxLength}.";

        return true;
    }

    public static Result<string> CertainLengthValidate(this string input, int length)
    {
        if (input.Length != length)
            return $"Length must be equal to {length}.";

        return true;
    }

    public static Result<string> NullValidate(this string? input)
    {
        if (input == null)
            return "Cannot be null.";

        return true;
    }

    public static Result<string> EmptyValidate(this string input)
    {
        if (input.Equals(string.Empty))
            return "Cannot be empty.";

        return true;
    }

    public static Result<string> WhiteSpacesValidate(this string input)
    {
        if (input.All(char.IsWhiteSpace))
            return "Cannot consist only of whitespace.";

        return true;
    }

    public static Result<string> CaseValidate(this string input, bool isMustBeUpperCase)
    {
        if (isMustBeUpperCase)
        {
            if (input != input.ToUpper())
                return "String must be in uppercase.";
        }
        else
        {
            if (input != input.ToLower())
                return "String must be in lowercase.";
        }

        return true;
    }

    public static Result<string> NullOrEmptyOrWhiteSpacesValidate(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return "Cannot be null, empty or consist only of whitespace.";

        return true;
    }

    public static Result<string> DefaultValidate(this string? input, int minLength, int maxLength)
    {
        var nullEmptyWhiteSpacesValidate = input.NullOrEmptyOrWhiteSpacesValidate();

        if (nullEmptyWhiteSpacesValidate.IsFailure)
            return nullEmptyWhiteSpacesValidate.Error;

        var minMaxLengthValidation = input!.MinMaxLengthValidate(minLength, maxLength);

        if (minMaxLengthValidation.IsFailure)
            return minMaxLengthValidation.Error;

        return true;
    }
}