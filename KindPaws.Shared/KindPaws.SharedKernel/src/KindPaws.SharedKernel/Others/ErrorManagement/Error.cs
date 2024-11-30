using KindPaws.SharedKernel.Enums;

namespace KindPaws.SharedKernel.Others.ErrorManagement;

public record Error
{
    private const string Separator = ";";

    private Error(
        string code,
        string message,
        ErrorType type,
        string? invalidPropertyName = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidPropertyName = invalidPropertyName;
    }

    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }
    public string? InvalidPropertyName { get; }

    public static Error Validation(string code, string message, string? invalidPropertyName = null)
    {
        return new Error(code, message, ErrorType.Validation, invalidPropertyName);
    }

    public static Error NotFound(string code, string message)
    {
        return new Error(code, message, ErrorType.NotFound);
    }

    public static Error Failure(string code, string message)
    {
        return new Error(code, message, ErrorType.Failure);
    }

    public static Error Conflict(string code, string message)
    {
        return new Error(code, message, ErrorType.Conflict);
    }

    public string SerializeToString()
    {
        return string.Join(Separator, Code, Message, Type);
    }

    public static Error Deserialize(string input)
    {
        var errorObjects = input.Split(Separator);

        if (errorObjects.Length < 3)
            throw new InvalidOperationException("invalid string not serializable");

        if (!Enum.TryParse<ErrorType>(errorObjects[2], out var errorType))
            throw new InvalidOperationException("Invalid not serializable error type");

        return new Error(
            errorObjects[0],
            errorObjects[1],
            errorType);
    }

    public ErrorList ToErrorList()
    {
        return new ErrorList([this]);
    }
}