namespace KindPaws.Domain.Shared.Others;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{name} is invalid");
        }

        public static Error RecordNotFound(string? name = null, object? identificator = null)
        {
            var label = name ?? "Record";
            var withIdentificator = identificator == null ? "" : $"with id {identificator}";
            return Error.Validation("record.is.not.found", $"{label} {withIdentificator} not found");
        }

        public static Error ValueWrongLength(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.length.is.invalid", $"invalid {label} length");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            var label = name ?? "Value";
            return Error.Validation("value.is.required", $"{label} is required");
        }
        
        public static Error RecordAlreadyExist(string? name = null)
        {
            var label = name ?? "Record";
            return Error.Validation("record.already.exist", $"{label} already exist");
        }
    }

    public static class Volunteer
    {
        
    }
}