namespace KindPaws.Auth.Contracts.Requests;

public record LoginRequest(
    string EmailAddress,
    string Password);