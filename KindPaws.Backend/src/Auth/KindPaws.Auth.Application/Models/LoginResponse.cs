namespace KindPaws.Auth.Application.Models;

public record LoginResponse(
    string AccessToken,
    Guid RefreshToken);