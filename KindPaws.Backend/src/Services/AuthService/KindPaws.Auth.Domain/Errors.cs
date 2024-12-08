using KindPaws.SharedKernel.Others.ErrorManagement;

namespace KindPaws.Auth.Domain;

public static class AuthErrors
{
    public static Error CredentialsAreInvalid()
    {
        return Error.Validation(
            "account.credentials.are.invalid",
            $"Account credentials are invalid");
    }

    public static Error ExpiredToken(string? tokenName = null)
    {
        tokenName ??= "Token";

        return Error.Validation(
            "token.is.expired",
            $"{tokenName} is expired");
    }

    public static Error TokenIsInvalid(string? tokenName = null)
    {
        tokenName ??= "Token";

        return Error.Validation(
            "token.is.invalid",
            $"{tokenName} is invalid");
    }
}