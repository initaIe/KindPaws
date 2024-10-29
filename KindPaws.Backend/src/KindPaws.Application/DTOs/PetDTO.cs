namespace KindPaws.Application.DTOs;

public class PetDTO
{
    public Guid Id { get; init; }
    public Guid SpecieId { get; init; }
    public Guid BreedId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? SupportStatus { get; init; }
    public string? Description { get; init; }
    public string? Color { get; init; }
    public int? Age { get; init; }
    public HealthDetailsDTO? HealthDetails { get; init; }
    public BiometricDetailsDTO? BiometricDetails { get; init; }
    public DateTime CreationDateTime { get; init; }
    public int Position { get; init; }
    public IEnumerable<PetPhotoDTO> Photos { get; init; } = [];
    public Guid VolunteerId { get; init; }
}