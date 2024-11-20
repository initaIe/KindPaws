namespace KindPaws.Volunteers.Contracts.Dtos;

public record PetPhotoDto
{
    public string Path { get; init; } = null!;
    public bool IsMain { get; init; }
}