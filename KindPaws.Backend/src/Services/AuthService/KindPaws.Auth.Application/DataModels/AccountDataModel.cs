using KindPaws.Auth.Contracts.Dtos;

namespace KindPaws.Auth.Application.DataModels;

public class AccountDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? LastModifiedAt { get; init; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string UserName { get; init; } = null!;

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string EmailAddress { get; init; } = null!;

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string? PhoneNumber { get; init; }

    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string PasswordHash { get; init; } = null!;
    public IReadOnlyList<Guid> Roles { get; init; } = [];
    public IReadOnlyList<RefreshSessionDto> RefreshSessions { get; init; } = [];
}