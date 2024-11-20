namespace KindPaws.Accounts.Contracts.Dtos;

public class AccountWithoutPasswordHashDto
{
    public Guid Id { get; init; }
    public string UserName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string? PhoneNumber { get; init; }
    public FullNameDto? FullName { get; init; }
    public DateTime CreationTimestamp { get; init; }
    public SocialNetworkDto[] SocialNetworks { get; init; } = [];
    public RefreshSessionDto[] RefreshSessions { get; init; } = [];
}