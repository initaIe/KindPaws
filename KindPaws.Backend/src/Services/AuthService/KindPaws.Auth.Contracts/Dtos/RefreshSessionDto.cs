namespace KindPaws.Auth.Contracts.Dtos;

public class RefreshSessionDto
{
    public DateTimeOffset CreatedAt { get; init; }
    public Guid Jti { get; init; }
    public Guid RefreshToken { get; init; }
    public DateTimeOffset ExpiresAt { get; init; }
}