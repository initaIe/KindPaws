using KindPaws.Auth.Contracts.Dtos;

namespace KindPaws.Auth.Application.DataModels;

public class AccountDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? LastModifiedAt { get; init; }
    public string UserName { get; init; } = null!;
    public string EmailAddress { get; init; } = null!;
    public string? PhoneNumber { get; init; }
    public IReadOnlyList<Guid> Roles { get; init; } = [];
    public IReadOnlyList<RefreshSessionDto> RefreshSessions { get; init; } = [];
}