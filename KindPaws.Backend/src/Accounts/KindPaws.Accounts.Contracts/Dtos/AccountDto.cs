namespace KindPaws.Accounts.Contracts.Dtos;

public class AccountDto
{
    public Guid Id { get; init; }
    public string UserName { get; init; } = null!;
    public string EmailAddress { get; init; } = null!;
    public string PasswordHash { get; init; } = null!;
    public string? PhoneNumber { get; init; }
    public FullNameDto? FullName { get; init; }
    public DateTime CreationTimestamp { get; init; }
    public SocialNetworkDto[] SocialNetworks { get; init; } = [];
    public RefreshSessionDto[] RefreshSessions { get; init; } = [];
    public AccountRoleDto[] AccountRoles { get; init; } = [];
}