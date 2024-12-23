using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KindPaws.Auth.Application.Abstractions;
using KindPaws.Auth.Application.Models;
using KindPaws.Auth.Domain;
using KindPaws.Auth.Infrastructure.Factories;
using KindPaws.Auth.Infrastructure.Options;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KindPaws.Auth.Infrastructure.Providers;

public class TokenProvider : ITokenProvider
{
    private readonly IOptionsMonitor<JwtBearerAuthOptions> _jwtBearerOptions;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    public TokenProvider(IOptionsMonitor<JwtBearerAuthOptions> options)
    {
        _jwtBearerOptions = options;
    }

    public string GenerateAccessToken(Guid accountId, Guid jti)
    {
        var claims = new[]
        {
            new Claim(CustomClaims.Sub, accountId.ToString()),
            new Claim(CustomClaims.Jti, jti.ToString()),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtBearerOptions.CurrentValue.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var nbfDateTime = DateTime.UtcNow;
        var expDateTime = DateTime.UtcNow.AddMinutes(_jwtBearerOptions.CurrentValue.ExpiresInMinutes);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtBearerOptions.CurrentValue.Issuer,
            audience: _jwtBearerOptions.CurrentValue.Audience,
            claims: claims,
            notBefore: nbfDateTime,
            expires: expDateTime,
            signingCredentials: signingCredentials);

        return _jwtSecurityTokenHandler.WriteToken(jwtToken);
    }

    public Result<AccessTokenParseResult, Error> ParseAccessToken(string token)
    {
        var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(token);

        if (jwtSecurityToken == null)
            return ErrorsAuth.TokenIsInvalid();

        var subClaim = jwtSecurityToken.Claims.FirstOrDefault(
            c => c.Type == CustomClaims.Sub)?.Value;

        if (!Guid.TryParse(subClaim, out var accountId))
            return ErrorsAuth.TokenIsInvalid();

        var jtiClaim = jwtSecurityToken.Claims.FirstOrDefault(
            c => c.Type == CustomClaims.Jti)?.Value;

        if (!Guid.TryParse(jtiClaim, out var jti))
            return ErrorsAuth.TokenIsInvalid();

        return new AccessTokenParseResult(accountId, jti);
    }

    public async Task<Result<Error>> ValidateAccessTokenWithoutLifeTimeAsync(string token)
    {
        var tokenValidationParameters = TokenValidationParametersFactory
            .CreateWithoutValidationLifeTime(_jwtBearerOptions.CurrentValue);

        var validationResult = await _jwtSecurityTokenHandler.ValidateTokenAsync(
            token,
            tokenValidationParameters);

        if (!validationResult.IsValid)
            return ErrorsAuth.TokenIsInvalid();

        return true;
    }
}