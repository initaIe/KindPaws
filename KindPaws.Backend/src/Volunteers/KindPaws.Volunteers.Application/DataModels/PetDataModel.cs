using KindPaws.Volunteers.Contracts.Dtos;

namespace KindPaws.Volunteers.Application.DataModels;

public class PetDataModel
{
    public Guid Id { get; init; }
    public Guid SpecieId { get; init; }
    public Guid BreedId { get; init; }
    public string Name { get; init; } = null!;
    public string? SupportStatus { get; init; }
    public string? Description { get; init; }
    public string? Color { get; init; }
    public DateTimeOffset? Birthday { get; init; }
    public HealthDetailsDto? HealthDetails { get; init; }
    public BiometricDetailsDto? BiometricDetails { get; init; }
    public DateTimeOffset CreationDateTime { get; init; }
    public int Position { get; init; }
    public IReadOnlyList<PetPhotoDto> Photos { get; init; } = [];
    public Guid VolunteerId { get; init; }
    public bool IsSoftDeleted { get; init; }
}