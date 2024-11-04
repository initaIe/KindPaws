using System.Text.Json.Serialization;
using KindPaws.SharedKernel.Others.ErrorManagement;
using KindPaws.SharedKernel.Others.ResultManagement;
using KindPaws.SharedKernel.Utilities.ValidationManagement.Validators;
using KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.Volunteers.Domain.ValueObjectsManagement.ValueObjects;

// TODO: in future add entity SocialNetwork with ID mb
public record SocialNetwork
{
    [JsonConstructor]
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