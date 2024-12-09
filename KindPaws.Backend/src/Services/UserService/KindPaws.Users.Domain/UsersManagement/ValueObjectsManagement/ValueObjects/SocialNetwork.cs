using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

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
        if (string.IsNullOrWhiteSpace(name))
            return GeneralErrors.ValueIsRequired(nameof(Name));

        name = name.Trim();

        if (!StringValidator.IsInRange(
                name,
                SocialNetworkConstraints.MinNameLength,
                SocialNetworkConstraints.MaxNameLength))
            return GeneralErrors.ValueOutOfRange(nameof(name));

        if (string.IsNullOrWhiteSpace(link))
            return GeneralErrors.ValueIsRequired(nameof(Link));

        link = link.Trim();

        if (!StringValidator.IsInRange(
                link,
                SocialNetworkConstraints.MinLinkLength,
                SocialNetworkConstraints.MaxLinkLength))
            return GeneralErrors.ValueOutOfRange(nameof(link));

        return new SocialNetwork(name, link);
    }
}