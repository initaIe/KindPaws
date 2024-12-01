namespace KindPaws.SharedKernel.Utilities.Extensions;

public static class StringExtensions
{
    public static string ToProperCase(this string input)
    {
        return char.ToUpper(input[0]) + input.Substring(1).ToLower();
    }
}