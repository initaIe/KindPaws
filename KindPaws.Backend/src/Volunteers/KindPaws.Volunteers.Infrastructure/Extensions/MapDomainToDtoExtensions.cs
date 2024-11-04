using KindPaws.Core.Dtos;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Volunteers.Infrastructure.Extensions;

public static class MapDomainToDtoExtensions
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
            healthDetails.Vaccines?.Select(vaccine => vaccine.Value),
            healthDetails.Diseases?.Select(disease => disease.Value),
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
}