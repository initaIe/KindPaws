namespace KindPaws.Users.Application.DataModels;

public class UserDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset LastModifiedAt { get; init; }
    public string UserName { get; init; } = null!;
    public string EmailAddress { get; init; } = null!;
    public ProfileDataModel Profile { get; init; } = null!;
    public Guid Roles { get; init; }
}