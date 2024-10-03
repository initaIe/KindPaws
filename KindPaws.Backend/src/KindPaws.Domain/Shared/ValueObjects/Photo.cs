namespace KindPaws.Domain.Shared.ValueObjects;

public record Photo
{
    // ef core
    private Photo()
    {
    }

    public Photo(PathToStorage pathToStorage)
    {
        PathToStorage = pathToStorage;
    }

    public PathToStorage PathToStorage { get; }
}