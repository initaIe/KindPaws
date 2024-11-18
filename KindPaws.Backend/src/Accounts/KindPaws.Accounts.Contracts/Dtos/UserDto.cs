namespace KindPaws.Accounts.Contracts.Dtos;

public class UserDto
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public FullNameDto? FullName { get; init; }
    public SocialNetworkDto[] SocialNetworks { get; init; }
}