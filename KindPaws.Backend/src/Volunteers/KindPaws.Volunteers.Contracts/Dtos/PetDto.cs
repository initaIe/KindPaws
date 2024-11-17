namespace KindPaws.Volunteers.Contracts.Dtos;

public class PetDto
{
    public Guid Id { get; init; }
    public Guid SpecieId { get; init; }
    public Guid BreedId { get; init; }
    public string Name { get; init; }
    public string? SupportStatus { get; init; }
    public string? Description { get; init; }
    public string? Color { get; init; }
    public DateOnly? Age { get; init; }
    public HealthDetailsDto? HealthDetails { get; init; }
    public BiometricDetailsDto? BiometricDetails { get; init; }
    public DateTime CreationDateTime { get; init; }
    public int Position { get; init; }
    public PetPhotoDto[] Photos { get; init; } = [];
    public Guid VolunteerId { get; init; }
    public bool IsSoftDeleted { get; init; }
}