namespace KindPaws.Accounts.Application.DataModels;

public class RefreshSessionDataModel
{
    public Guid Id { get; init; }
    public Guid Jti { get; init; }
    public Guid RefreshToken { get; init; }
    public DateTime CreationTimestamp { get; init; }
    public DateTime ExpireTimestamp { get; init; }
    public Guid AccountId { get; init; }
}