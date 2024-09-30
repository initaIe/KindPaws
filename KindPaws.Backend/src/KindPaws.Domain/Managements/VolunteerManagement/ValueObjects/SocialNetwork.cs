using KindPaws.Domain.Managements.VolunteerManagement.Constraints;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;

namespace KindPaws.Domain.Managements.VolunteerManagement.ValueObjects;

// TODO: in future add entity SocialNetwork with ID mb
public record SocialNetwork
{
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

    public static Result<SocialNetwork, IEnumerable<string>> Create(
        string name,
        string link)
    {
        List<string> errors = [];

        name.DefaultValidate(
                SocialNetworkConstraints.MinNameLength,
                SocialNetworkConstraints.MaxNameLength)
            .AddErrorIfFailure(errors);

        link.DefaultValidate(
                SocialNetworkConstraints.MinLinkLength,
                SocialNetworkConstraints.MaxLinkLength)
            .AddErrorIfFailure(errors);

        if (errors.Count > 0)
            return errors;

        return new SocialNetwork(
            name,
            link);
    }
}