namespace KindPaws.Accounts.Domain;

public class RefreshSession
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid Jti { get; set; }
    public Guid RefreshToken { get; set; }
    public DateTime ExpiresIn { get; set; }
    public DateTime CreatedAt { get; set; }
}