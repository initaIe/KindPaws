using KindPaws.Pets.Contracts.Dtos;

namespace KindPaws.Pets.Application.DataModels;

public class PetDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? LastModifiedAt { get; init; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string Name { get; init; } = null!;
    public Guid SpecieId { get; init; }
    public Guid BreedId { get; init; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string SupportStatus { get; init; } = null!;

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string? Description { get; init; }
    public DateTimeOffset? BirthdayAt { get; init; }
    public HealthDetailsDto? HealthDetails { get; init; }
    public BiometricDetailsDto? BiometricDetails { get; init; }
    public Guid VolunteerId { get; init; }
}