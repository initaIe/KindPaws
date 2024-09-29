namespace KindPaws.Domain.Managements.VolunteerManagement.ValueObjects.Lists;

public record SocialNetworkList
{
    private readonly List<SocialNetwork> _socialNetworks;

    public SocialNetworkList()
    {
    }

    public SocialNetworkList(List<SocialNetwork> socialNetworks)
    {
        _socialNetworks = socialNetworks;
    }

    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
}