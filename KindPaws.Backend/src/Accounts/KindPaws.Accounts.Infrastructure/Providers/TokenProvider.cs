using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.Models;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.Accounts.Infrastructure.Factories;
using KindPaws.Accounts.Infrastructure.Options;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KindPaws.Accounts.Infrastructure.Providers;

public class TokenProvider : ITokenProvider
{
    private readonly JwtBearerOptions _jwtBearerOptions;

    public TokenProvider(
        IOptions<JwtBearerOptions> options)
    {
        _jwtBearerOptions = options.Value;
    }

    public string GetAccessToken(
        string userId,
        string userEmail,
        string jti)
    {
        var subClaim = userId.ToString();
        var jtiClaim = jti.ToString();

        var claims = new[]
        {
            new Claim(CustomClaims.Sub, subClaim),
            new Claim(CustomClaims.Email, userEmail),
            new Claim(CustomClaims.Jti, jtiClaim),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtBearerOptions.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var nbfDateTime = DateTime.UtcNow;
        var expDateTime = DateTime.UtcNow.AddMinutes(_jwtBearerOptions.ExpiresInMinutes);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtBearerOptions.Issuer,
            audience: _jwtBearerOptions.Audience,
            claims: claims,
            notBefore: nbfDateTime,
            expires: expDateTime,
            signingCredentials: signingCredentials);

        var jwtAccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return jwtAccessToken;
    }

    public async Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaimsAsync(
        string jwtAccessToken,
        CancellationToken cancellationToken = default)
    {
        var jwtHandler = new JwtSecurityTokenHandler();

        var tokenValidationParameters = TokenValidationParametersFactory
            .CreateWithoutValidationLifeTime(_jwtBearerOptions);

        var validationResult = await jwtHandler.ValidateTokenAsync(
            jwtAccessToken,
            tokenValidationParameters);

        if (!validationResult.IsValid)
            return Errors.Accounts.TokenIsInvalid();

        return validationResult.ClaimsIdentity.Claims.ToList();
    }
}