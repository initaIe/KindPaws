using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

namespace KindPaws.Application.DTOs;

public record SocialNetworkDTO(
    string Name,
    string Link)
{
    public static SocialNetworkDTO GetFromDomainModel(SocialNetwork socialNetwork)
        => new(socialNetwork.Name, socialNetwork.Link);
}