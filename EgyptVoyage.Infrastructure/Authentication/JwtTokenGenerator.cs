using EgyptVoyage.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace EgyptVoyage.Infrastructure.Authentication;

/// <summary>
/// JWT token generator service
/// </summary>
public class JwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    /// <summary>
    /// Generate JWT token for tourist
    /// </summary>
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

    /// <summary>
    /// Generate JWT token for clerk
    /// </summary>
    public string GenerateTokenForClerk(Clerk clerk)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, clerk.Id),
            new Claim(JwtRegisteredClaimNames.Email, clerk.Email),
            new Claim(JwtRegisteredClaimNames.Name, clerk.Name),
            new Claim(ClaimTypes.Role, clerk.Role.ToString()),
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