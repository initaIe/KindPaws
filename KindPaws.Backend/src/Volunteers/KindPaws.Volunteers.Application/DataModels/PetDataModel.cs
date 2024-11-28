using KindPaws.Volunteers.Contracts.Dtos;

namespace KindPaws.Volunteers.Application.DataModels;

public class PetDataModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Guid SpecieId { get; init; }
    public Guid BreedId { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public string? SupportStatus { get; init; }
    public string? Description { get; init; }
    public string? Color { get; init; }
    public DateTimeOffset? Birthday { get; init; }
    public HealthDetailsDto? HealthDetails { get; init; }
    public BiometricDetailsDto? BiometricDetails { get; init; }
    public IReadOnlyList<PetPhotoDto> Photos { get; init; } = [];
    public int Position { get; init; }
    public bool IsSoftDeleted { get; init; }
    public DateTimeOffset? SoftDeletedAt { get; init; }
    public Guid VolunteerId { get; init; }
}