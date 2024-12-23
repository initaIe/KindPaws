namespace KindPaws.Auth.Application.Common.DataModels;

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
    public Guid[] Roles { get; init; } = [];
    public IReadOnlyList<RefreshSessionDataModel> RefreshSessions { get; init; } = [];
}