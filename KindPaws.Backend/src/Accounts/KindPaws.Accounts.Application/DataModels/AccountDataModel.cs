using KindPaws.Accounts.Contracts.Dtos;

namespace KindPaws.Accounts.Application.DataModels;

public class AccountDataModel
{
    public Guid Id { get; init; }
    public string UserName { get; init; } = null!;
    public string EmailAddress { get; init; } = null!;
    public string PasswordHash { get; init; } = null!;
    public string? PhoneNumber { get; init; }
    public FullNameDto? FullName { get; init; }
    public DateTime CreationTimestamp { get; init; }
    public IReadOnlyList<SocialNetworkDto> SocialNetworks { get; init; } = [];
    public IReadOnlyList<RefreshSessionDataModel> RefreshSessions { get; init; } = [];
    public IReadOnlyList<Guid> Roles { get; init; } = [];
}