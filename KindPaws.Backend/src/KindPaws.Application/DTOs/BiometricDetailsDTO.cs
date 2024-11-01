using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

namespace KindPaws.Application.DTOs;

public record BiometricDetailsDTO(
    float? Height,
    float? Weight,
    string? Gender)
{
    public static BiometricDetailsDTO GetFromDomainModel(BiometricDetails biometricDetails)
        => new (biometricDetails.Height?.Value, biometricDetails.Weight?.Value, biometricDetails.Gender?.Value);
}