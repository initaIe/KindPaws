namespace KindPaws.Domain.Shared.ValueObjects;

public record Photo
{
    public Photo(string path)
    {
        Path = path;
    }

    public string Path { get; }
}