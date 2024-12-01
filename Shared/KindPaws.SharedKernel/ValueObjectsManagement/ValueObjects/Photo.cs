namespace KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

public record Photo
{
    public Photo(FilePath filePath)
    {
        FilePath = filePath;
    }

    public FilePath FilePath { get; }
}