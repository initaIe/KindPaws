using System.Text.Json.Serialization;
using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjectsConstraints;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Utilities.Validators;

namespace KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;

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
            return Errors.General.ValueIsRequired(nameof(Name));

        name = name.Trim();

        if (!StringValidator.IsInRange(
                name,
                SocialNetworkConstraints.MinNameLength,
                SocialNetworkConstraints.MaxNameLength))
            return Errors.General.ValueOutOfRange(nameof(name));

        if (string.IsNullOrWhiteSpace(link))
            return Errors.General.ValueIsRequired(nameof(Link));

        link = link.Trim();

        if (!StringValidator.IsInRange(
                link,
                SocialNetworkConstraints.MinLinkLength,
                SocialNetworkConstraints.MaxLinkLength))
            return Errors.General.ValueOutOfRange(nameof(link));

        return new SocialNetwork(name, link);
    }
}