using KindPaws.Domain.Managements.VolunteersManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;

// TODO: in future add entity SocialNetwork with ID mb
public record SocialNetwork
{
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
        name = name.Trim();

        if (!StringValidator.IsInRange(
                name,
                SocialNetworkConstraints.MinNameLength,
                SocialNetworkConstraints.MaxNameLength))
            return Errors.General.ValueWrongLength(nameof(name));

        link = link.Trim();

        if (!StringValidator.IsInRange(
                link,
                SocialNetworkConstraints.MinLinkLength,
                SocialNetworkConstraints.MaxLinkLength))
            return Errors.General.ValueWrongLength(nameof(link));

        return new SocialNetwork(name, link);
    }
}