using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KindPaws.Auth.Infrastructure.Factories;
using KindPaws.Auth.Infrastructure.Options;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KindPaws.Auth.Infrastructure;

public class TokenProvider
{
    private readonly JwtBearerOptions _jwtBearerOptions;

    public TokenProvider(IOptions<JwtBearerOptions> options)
    {
        _jwtBearerOptions = options.Value;
    }
    
    public string GenerateAccessToken(Guid accountId, Guid jti)
    {
        var claims = new[]
        {
            new Claim(CustomClaims.Sub, accountId.ToString()),
            new Claim(CustomClaims.Jti, jti.ToString()),
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

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
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