namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;

public record SocialNetworkList
{
    private readonly List<SocialNetwork> _socialNetworks;

    // ef core
    private SocialNetworkList()
    {
    }

    public SocialNetworkList(List<SocialNetwork> socialNetworks)
    {
        _socialNetworks = socialNetworks;
    }

    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
}