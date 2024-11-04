namespace KindPaws.SharedKernel.Utilities.ValidationManagement.Validators;

public static class StringValidator
{
    public static bool IsInRange(string input, int minLength, int maxLength)
    {
        return input.Length >= minLength && input.Length <= maxLength;
    }

    public static bool IsLength(string input, int length)
    {
        return input.Length == length;
    }

    public static bool IsNull(string? input)
    {
        return input == null;
    }

    public static bool IsEmpty(string input)
    {
        return input == string.Empty;
    }

    public static bool IsWhiteSpace(string input)
    {
        return input.All(char.IsWhiteSpace);
    }

    public static bool IsUpperCase(string input)
    {
        return input == input.ToUpper();
    }

    public static bool IsLowerCase(string input)
    {
        return input == input.ToLower();
    }

    public static bool IsAlphabetic(string input)
    {
        return input.All(char.IsLetter);
    }

    public static bool IsAlphabeticWithSpaces(string input)
    {
        return input.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
    }
}