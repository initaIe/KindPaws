using System.Text.Json;
using KindPaws.Core.Dtos;
using KindPaws.Core.Dtos.DapperDtos;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Application.Helpers;

public static class VolunteerMappers
{
    public static AddressDto ToDto(this Address address)
        => new(address.City, address.Street);

    public static SocialNetworkDto ToDto(this SocialNetwork socialNetwork)
        => new(socialNetwork.Name, socialNetwork.Link);

    public static RequisiteDto ToDto(this Requisite requisite)
        => new(requisite.Name, requisite.Description);

    public static HealthDetailsDto ToDto(this HealthDetails healthDetails)
        => new(
            healthDetails.Description?.Value,
            healthDetails.Vaccines?.Select(vaccine => vaccine.Value) ?? [],
            healthDetails.Diseases?.Select(disease => disease.Value) ?? [],
            healthDetails.HealthStatus?.Value,
            healthDetails.IsNeutered);

    public static BiometricDetailsDto ToDto(this BiometricDetails biometricDetails)
        => new(
            biometricDetails.Height?.Value,
            biometricDetails.Weight?.Value,
            biometricDetails.Gender?.Value);

    public static PetPhotoDto ToDto(this PetPhoto petPhoto)
        => new(
            petPhoto.Photo.FilePath.Value,
            petPhoto.IsMain);

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
            Age = petDapperDto.DateBirth != null ? DateOnly.FromDateTime(petDapperDto.DateBirth!.Value) : null,
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