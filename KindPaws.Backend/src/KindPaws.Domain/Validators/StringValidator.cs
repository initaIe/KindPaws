using CSharpFunctionalExtensions;

namespace KindPaws.Domain.Validators;

public static class StringValidator
{
    public static Result<string, string> MinMaxLengthValidate(this string input, int minLength, int maxLength)
    {
        if (input.Length < minLength)
        {
            var error = $"Length must be larger than or equal {minLength}.";
            return Result.Failure<string, string>(error);
        }

        if (input.Length > maxLength)
        {
            var error = $"Length must be smaller than or equal {maxLength}.";
            return Result.Failure<string, string>(error);
        }

        return Result.Success<string, string>(input);
    }

    public static Result<string, string> CertainLengthValidate(this string input, int length)
    {
        if (input.Length != length)
        {
            var error = $"Length must be equal to {length}.";
            return Result.Failure<string, string>(error);
        }

        return Result.Success<string, string>(input);
    }

    public static Result<string, string> NullValidate(this string? input)
    {
        if (input == null)
        {
            var error = "Cannot be null.";
            return Result.Failure<string, string>(error);
        }

        return Result.Success<string, string>(input);
    }

    public static Result<string, string> EmptyValidate(this string input)
    {
        if (input.Equals(string.Empty))
        {
            var error = "Cannot be empty.";
            return Result.Failure<string, string>(error);
        }

        return Result.Success<string, string>(input);
    }

    public static Result<string, string> WhiteSpacesValidate(this string input)
    {
        if (input.All(char.IsWhiteSpace))
        {
            var error = "Cannot consist only of whitespace.";
            return Result.Failure<string, string>(error);
        }

        return Result.Success<string, string>(input);
    }

    public static Result<string, string> CaseValidate(this string input, bool isMustBeUpperCase)
    {
        if (isMustBeUpperCase)
        {
            if (input != input.ToUpper())
            {
                var error = "String must be in uppercase.";
                return Result.Failure<string, string>(error);
            }
        }
        else
        {
            if (input != input.ToLower())
            {
                var error = "String must be in lowercase.";
                return Result.Failure<string, string>(error);
            }
        }

        return Result.Success<string, string>(input);
    }

    public static Result<string, string> NullEmptyWhiteSpacesValidate(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            var error = "Cannot be null, empty or consist only of whitespace.";
            return Result.Failure<string, string>(error);
        }

        return Result.Success<string, string>(input);
    }

    public static Result<string, string> DefaultValidate(this string? input, int minLength, int maxLength)
    {
        var nullEmptyWhiteSpacesValidate = input.NullEmptyWhiteSpacesValidate();

        if (nullEmptyWhiteSpacesValidate.IsFailure)
            return Result.Failure<string, string>(nullEmptyWhiteSpacesValidate.Error);

        var minMaxLengthValidation = input!.MinMaxLengthValidate(minLength, maxLength);

        if (minMaxLengthValidation.IsFailure)
            return Result.Failure<string, string>(minMaxLengthValidation.Error);

        return Result.Success<string, string>(input!);
    }
}