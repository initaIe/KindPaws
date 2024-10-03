namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;

public record SocialNetworkList
{
    // ef core
    private SocialNetworkList()
    {
    }

    public SocialNetworkList(List<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }

    public IReadOnlyList<SocialNetwork> SocialNetworks { get; }
}