using System.Text.Json;
using KindPaws.Core.Dtos.DapperDtos;
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

    public static IEnumerable<RequisiteDto> ToDtoCollection(this IEnumerable<Requisite> requisites)
        => requisites.Select(ToDto);

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

    public static IEnumerable<PetPhotoDto> ToDtoCollection(this IEnumerable<PetPhoto> petPhotos)
        => petPhotos.Select(ToDto);


    public static PetDto ToDto(this PetDapperDto petDapperDto)
        => new PetDto
        {
            Id = petDapperDto.Id,
            SpecieId = petDapperDto.SpecieId,
            BreedId = petDapperDto.BreedId,
            Name = petDapperDto.Name,
            SupportStatus = petDapperDto.SupportStatus,
            Description = petDapperDto.Description,
            Color = petDapperDto.Color,
            Birthday = petDapperDto.DateBirth,
            HealthDetails = JsonSerializer.Deserialize<HealthDetails>(petDapperDto.HealthDetails)!.ToDto(),
            BiometricDetails = JsonSerializer.Deserialize<BiometricDetails>(petDapperDto.BiometricDetails)!.ToDto(),
            CreationDateTime = petDapperDto.CreationDateTime,
            Position = petDapperDto.Position,
            Photos = JsonSerializer.Deserialize<IEnumerable<PetPhoto>>(petDapperDto.Photos,
                    JsonSerializerOptions.Default)!
                .Select(petPhoto => petPhoto.ToDto()).ToArray(),
            VolunteerId = petDapperDto.VolunteerId,
            IsSoftDeleted = petDapperDto.IsSoftDeleted,
        };
}