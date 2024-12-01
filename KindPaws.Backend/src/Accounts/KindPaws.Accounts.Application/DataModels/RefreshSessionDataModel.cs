namespace KindPaws.Accounts.Application.DataModels;

public class RefreshSessionDataModel
{
    public Guid Id { get; init; }
    public Guid Jti { get; init; }
    public Guid RefreshToken { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset ExpiresAt { get; init; }
    public Guid AccountId { get; init; }
}