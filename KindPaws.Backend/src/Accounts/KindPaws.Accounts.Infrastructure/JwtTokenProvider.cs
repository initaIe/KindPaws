using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KindPaws.Accounts.Application.Interfaces;
using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Infrastructure.Options;
using KindPaws.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KindPaws.Accounts.Infrastructure;

public class JwtTokenProvider : ITokenProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenProvider(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }

    public string GenerateAccessToken(User user)
    {
        var nbfDateTime = DateTime.UtcNow.AddMinutes(-1);
        var expDateTime = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtOptions.ExpiredMinutesTime));
        var nbfTimeStamp = ((DateTimeOffset)nbfDateTime).ToUnixTimeSeconds();
        var expTimeStamp = ((DateTimeOffset)expDateTime).ToUnixTimeSeconds();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Exp, expTimeStamp.ToString()),
            new Claim(JwtRegisteredClaimNames.Nbf, nbfTimeStamp.ToString())
        };


        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            notBefore: nbfDateTime,
            expires: expDateTime,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}