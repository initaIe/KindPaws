namespace KindPaws.Domain.Shared.Others;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            name ??= "value";
            return Error.Validation("value.is.invalid", $"{name} is invalid");
        }

        public static Error RecordNotFound(
            string? name = null,
            string? propertyName = null,
            object? value = null)
        {
            name ??= "Record";
            propertyName = propertyName == null ? "with" : $"with property {propertyName}";
            value ??= "value";
            return Error.Validation("record.is.not.found", $"{name} {propertyName} {value} not found");
        }

        public static Error ValueWrongLength(string? name = null)
        {
            name ??= "value";
            return Error.Validation("value.length.is.invalid", $"invalid {name} length");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            name ??= "Value";
            return Error.Validation("value.is.required", $"{name} is required");
        }

        public static Error RecordAlreadyExist(string? name = null, string? propertyName = null)
        {
            name ??= "Record";
            propertyName = propertyName == null ? string.Empty : $"with property {propertyName}";
            return Error.Validation("record.already.exist", $"{name} {propertyName} already exist");
        }
    }
}