using EgyptVoyage.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EgyptVoyage.Infrastructure.Authentication;

public class JwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    // بيعمل Token للـ Tourist
    public string GenerateTokenForTourist(Tourist tourist)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, tourist.Id),
            new Claim(JwtRegisteredClaimNames.Email, tourist.Email),
            new Claim(JwtRegisteredClaimNames.Name, tourist.Name),
            new Claim(ClaimTypes.Role, "Tourist"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        return GenerateToken(claims);
    }

    // بيعمل Token للـ Clerk
    // Role = "Clerk" عشان يقدر يوصل لـ CRUD Endpoints
    public string GenerateTokenForClerk(Clerk clerk)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, clerk.Id),
            new Claim(JwtRegisteredClaimNames.Email, clerk.Email),
            new Claim(JwtRegisteredClaimNames.Name, clerk.Name),
            new Claim(ClaimTypes.Role, "Clerk"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        return GenerateToken(claims);
    }

    private string GenerateToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
