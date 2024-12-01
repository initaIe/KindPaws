using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Volunteers.Contracts.Dtos;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Mappers;

public static class ToDtoMappers
{
    public static AddressDto ToDto(this Address address)
        => new AddressDto
        {
            City = address.City,
            Street = address.Street
        };

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


    public static PetPhotoDto ToDto(this PetPhoto petPhoto)
        => new PetPhotoDto
        {
            Path = petPhoto.Photo.FilePath.Value,
            IsMain = petPhoto.IsMain
        };

    public static IReadOnlyList<PetPhotoDto> ToDtoCollection(this IEnumerable<PetPhoto> petPhotos)
        => petPhotos.Select(ToDto).ToList();
}