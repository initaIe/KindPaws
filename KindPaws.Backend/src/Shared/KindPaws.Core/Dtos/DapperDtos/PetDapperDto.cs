namespace KindPaws.Core.Dtos.DapperDtos;

public class PetDapperDto
{
    public Guid Id { get; init; }
    public Guid SpecieId { get; init; }
    public Guid BreedId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? SupportStatus { get; init; }
    public string? Description { get; init; }
    public string? Color { get; init; }
    public DateTime? DateBirth { get; init; }
    public string HealthDetails { get; init; } = string.Empty;
    public string BiometricDetails { get; init; } = string.Empty;
    public DateTime CreationDateTime { get; init; }
    public int Position { get; init; }
    public string Photos { get; init; }= string.Empty;
    public Guid VolunteerId { get; init; }
    public bool IsSoftDeleted { get; init; }
}