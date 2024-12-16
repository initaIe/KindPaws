namespace KindPaws.Auth.Contracts.Requests;

public record RegisterRequest(
    string Username,
    string EmailAddress,
    string Password);