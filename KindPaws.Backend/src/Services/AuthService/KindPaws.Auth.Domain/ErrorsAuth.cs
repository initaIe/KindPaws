using KindPaws.SharedKernel.ErrorManagement;

namespace KindPaws.Auth.Domain;

public static class ErrorsAuth
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

    public static Error RegistrationFailure(Guid errorId)
    {
        return Error.Failure(
            "registration.was.failure",
            $"Failed to register account. Error id: {errorId}");
    }

    public static Error LoginFailure(Guid errorId)
    {
        return Error.Failure(
            "login.was.failure",
            $"Failed to login. Error id: {errorId}");
    }

    public static Error RefreshTokensFailure(Guid errorId)
    {
        return Error.Failure(
            "refresh.tokens.was.failure",
            $"Failed to refresh tokens. Error id: {errorId}");
    }
}