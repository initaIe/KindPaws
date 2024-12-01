namespace KindPaws.Accounts.Contracts.Requests;

public record CreateAccountRequest(
    string UserName,
    string EmailAddress,
    string Password);