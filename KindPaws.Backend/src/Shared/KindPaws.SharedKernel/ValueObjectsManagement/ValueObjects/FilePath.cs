using System.Text.Json.Serialization;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;

namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record FilePath
{
    //TODO: валидация + сделать приватным
    [JsonConstructor]
    private FilePath(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<FilePath, Error> Create(
        string path,
        string extension)
    {
        return new FilePath($"{path}{extension}");
    }

    public static Result<FilePath, Error> Create(
        Guid path,
        string extension)
    {
        return new FilePath($"{path.ToString()}{extension}");
    }

    public static Result<FilePath, Error> Create(string input)
    {
        return new FilePath(input);
    }
}