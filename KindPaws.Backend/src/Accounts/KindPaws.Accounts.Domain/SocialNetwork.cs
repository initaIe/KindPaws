using System.Text.Json.Serialization;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Accounts.Domain;

public record SocialNetwork
{
    [JsonConstructor]
    public SocialNetwork()
    {
    }

    private SocialNetwork(
        string name,
        string link)
    {
        Name = name;
        Link = link;
    }

    public string Name { get; }
    public string Link { get; }

    public static Result<SocialNetwork, Error> Create(
        string name,
        string link)
    {
        return new SocialNetwork(name, link);
    }
}