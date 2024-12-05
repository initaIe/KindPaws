using KindPaws.Pets.Contracts.Dtos;
using KindPaws.Pets.Domain.VolunteersManagement.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Pets.Application.Mappers;

public static class ToDtoMappers
{
    public static RequisiteDto ToDto(this Requisite requisite)
        => new RequisiteDto
        {
            Name = requisite.Name,
            Description = requisite.Description
        };

    public static IReadOnlyList<RequisiteDto> ToDtoCollection(this IEnumerable<Requisite> requisites)
        => requisites.Select(ToDto).ToList();

    public static HealthDetailsDto ToDto(this HealthDetails healthDetails)
        => new HealthDetailsDto
        {
            Description = healthDetails.Description?.Value,
            Vaccines = healthDetails.Vaccines.Select(v => v.Value) ?? [],
            Diseases = healthDetails.Diseases.Select(disease => disease.Value) ?? [],
            HealthStatus = healthDetails.HealthStatus?.Value,
            IsNeutered = healthDetails.IsNeutered
        };


    public static BiometricDetailsDto ToDto(this BiometricDetails biometricDetails)
        => new BiometricDetailsDto
        {
            Height = biometricDetails.Height?.Value,
            Weight = biometricDetails.Weight?.Value,
            Gender = biometricDetails.Gender?.Value
        };
}