namespace KindPaws.Accounts.Contracts.Requests;

public record ValidateAccountByEmailAddressRequest(
    string EmailAddress,
    string Password);