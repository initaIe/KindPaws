namespace KindPaws.Users.Application.DataModels;

public class UserDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset LastModifiedAt { get; init; }
    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string UserName { get; init; } = null!;
    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string EmailAddress { get; init; } = null!;
    public ProfileDataModel Profile { get; init; } = null!;
    public IReadOnlyList<Guid> Roles { get; init; } = [];
}