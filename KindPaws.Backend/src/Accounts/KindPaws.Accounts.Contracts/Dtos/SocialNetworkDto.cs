namespace KindPaws.Accounts.Contracts.Dtos;

public record SocialNetworkDto
{
    public string Name { get; init; } = null!;
    public string Link { get; init; } = null!;
}