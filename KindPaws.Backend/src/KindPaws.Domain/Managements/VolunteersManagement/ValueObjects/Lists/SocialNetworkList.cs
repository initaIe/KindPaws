namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;

public record SocialNetworkList
{
    // ef core
    private SocialNetworkList()
    {
    }

    public SocialNetworkList(IEnumerable<SocialNetwork> socialNetworks)
    {
        SocialNetworks = socialNetworks.ToList();
    }

    public IReadOnlyList<SocialNetwork> SocialNetworks { get; }
}