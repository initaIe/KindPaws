namespace KindPaws.SharedKernel.Others.ErrorManagement;

public static class GeneralErrors
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

    public static Error ValueOutOfRange(string? name = null)
    {
        name ??= "value";
        return Error.Validation("value.out.of.range", $"{name} out of range");
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

    public static Error ValueFormatIsInvalid(string? name = null)
    {
        name ??= "Value";
        return Error.Validation("value.format.is.invalid", $"{name} format is invalid");
    }

    public static Error ValueCharacterSetIsInvalid(string? name = null)
    {
        name ??= "Value";
        return Error.Validation("value.character.set.is.invalid", $"{name} character set is invalid");
    }

    public static Error OperationCanNotBePerformed(string? operationName = null, string? text = null)
    {
        operationName ??= "Operation";
        text ??= "";
        return Error.InvalidOperation(
            "operation.can.not.be.performed",
            $"{operationName} can not be performed {text}");
    }
}