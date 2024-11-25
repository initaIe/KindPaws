namespace KindPaws.Auth.Contracts.Requests;

public record LoginByEmailAddressRequest(string EmailAddress, string Password);