using KindPaws.Accounts.Domain.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Accounts.Application.Models;

public record AccessTokenAndJti(
    string AccessToken,
    Jti Jti);