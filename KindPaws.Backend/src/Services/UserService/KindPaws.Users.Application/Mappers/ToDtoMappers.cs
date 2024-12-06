using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Contracts.Dtos;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Users.Application.Mappers;

public static class ToDtoMappers
{
    public static FullNameDto ToDto(this FullName fullName)
        => new FullNameDto
        {
            FirstName = fullName.FirstName,
            LastName = fullName.LastName,
            Patronymic = fullName.Patronymic
        };

    public static AddressDto ToDto(this Address address)
        => new AddressDto
        {
            Country = address.Country,
            City = address.City,
            Street = address.Street
        };


    public static SocialNetworkDto ToDto(this SocialNetwork socialNetwork)
        => new SocialNetworkDto
        {
            Name = socialNetwork.Name,
            Link = socialNetwork.Link
        };

    public static IReadOnlyList<SocialNetworkDto> ToDtoCollection(this IEnumerable<SocialNetwork> socialNetworks)
        => socialNetworks.Select(ToDto).ToList();

    public static Guid ToGuid(this RoleId roleId)
        => roleId.Value;

    public static IReadOnlyList<Guid> ToDtoCollection(this IEnumerable<RoleId> roles)
        => roles.Select(ToGuid).ToList();
}