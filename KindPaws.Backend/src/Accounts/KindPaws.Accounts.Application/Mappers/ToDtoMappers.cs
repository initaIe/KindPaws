using KindPaws.Accounts.Contracts.Dtos;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Accounts.Application.Mappers;

public static class ToDtoMappers
{
    public static FullNameDto ToDto(this FullName fullName)
        => new FullNameDto
        {
            FirstName = fullName.FirstName,
            LastName = fullName.LastName,
            Patronymic = fullName.Patronymic
        };


    public static SocialNetworkDto ToDto(this SocialNetwork socialNetwork)
        => new SocialNetworkDto
        {
            Name = socialNetwork.Name,
            Link = socialNetwork.Link
        };

    public static IEnumerable<SocialNetworkDto> ToDtoCollection(this IEnumerable<SocialNetwork> socialNetworks)
        => socialNetworks.Select(ToDto);
}