using KindPaws.Accounts.Domain;

namespace KindPaws.Accounts.Application.Abstractions;

public interface ITokenProvider
{
    string GenerateAccessToken(User user);
}