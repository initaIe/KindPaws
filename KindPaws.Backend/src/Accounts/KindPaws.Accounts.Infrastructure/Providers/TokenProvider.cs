using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KindPaws.Accounts.Application.Abstractions;
using KindPaws.Accounts.Application.Models;
using KindPaws.Accounts.Domain;
using KindPaws.Accounts.Infrastructure.DbContexts;
using KindPaws.Accounts.Infrastructure.Options;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Others.ErrorManagement;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KindPaws.Accounts.Infrastructure.Providers;

public class TokenProvider : ITokenProvider
{
    private readonly AccountsWriteDbContext _dbContext;
    private readonly JwtBearerOptions _jwtBearerOptions;
    private readonly RefreshTokenOptions _refreshTokenOptions;

    public TokenProvider(
        IOptions<JwtBearerOptions> options,
        IOptions<RefreshTokenOptions> refreshTokenOptions,
        AccountsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
        _refreshTokenOptions = refreshTokenOptions.Value;
        _jwtBearerOptions = options.Value;
    }

    public JwtAccessTokenCreationResult GenerateAccessToken(User user)
    {
        var jti = Guid.NewGuid();

        var claims = new[]
        {
            new Claim(CustomClaims.Sub, user.Id.ToString()),
            new Claim(CustomClaims.Email, user.Email!),
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

        var jwtAccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return new JwtAccessTokenCreationResult(jwtAccessToken, jti);
    }

    // TODO: токен провайдер должен только возвращать + создавать токен. крч чет придумать и рефаторить
    public async Task<Guid> GenerateRefreshTokenAsync(User user, Guid jti,
        CancellationToken cancellationToken = default)
    {
        var refreshSession = new RefreshSession
        {
            User = user,
            Jti = jti,
            CreatedAt = DateTime.UtcNow,
            ExpiresIn = DateTime.UtcNow.AddDays(_refreshTokenOptions.ExpiresInDays),
            RefreshToken = Guid.NewGuid()
        };

        await _dbContext.RefreshSessions.AddAsync(refreshSession, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return refreshSession.RefreshToken;
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