namespace KindPaws.Auth.Contracts.Requests;

public record RegisterRequest(
    string UserName,
    string EmailAddress,
    string Password);