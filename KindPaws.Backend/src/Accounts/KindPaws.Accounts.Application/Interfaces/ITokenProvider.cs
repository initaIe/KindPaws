using KindPaws.Accounts.Domain;

namespace KindPaws.Accounts.Application.Interfaces;

public interface ITokenProvider
{
    string GenerateAccessToken(User user);
}