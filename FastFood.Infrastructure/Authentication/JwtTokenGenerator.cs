using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FastFood.Application.Common.Interfaces.Authentication;
using FastFood.Application.Common.Interfaces.Services;
using FastFood.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FastFood.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _JwtSettings;
    private readonly IDateTieProviders _DateTimeProvider;

    public JwtTokenGenerator(IDateTieProviders dateTimeProvider, IOptions<JwtSettings> jwtSettings)
    {
        _DateTimeProvider = dateTimeProvider;
        _JwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_JwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: _JwtSettings.Issuer,
            audience: _JwtSettings.Audience,
            expires: _DateTimeProvider.UtcNow.AddMinutes(_JwtSettings.ExpirationMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}