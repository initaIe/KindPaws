namespace KindPaws.Pets.Contracts.Dtos;

public record BiometricDetailsDto
{
    public float? Height { get; init; }
    public float? Weight { get; init; }
    public string? Gender { get; init; }
}