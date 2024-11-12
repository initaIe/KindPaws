namespace KindPaws.Accounts.Contracts.Requests;

public record RegisterRequest(
    string Email,
    string UserName,
    string Password);