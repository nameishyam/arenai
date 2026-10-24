using Server.Application.Interfaces.Service;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Application.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Infrastructure
{
    public class JwtService(IOptions<JwtConfiguration> jwtConfigurations) : IJwtService
    {
        public string GenerateToken(Guid userId, string email)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, email)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfigurations.Value.SecretKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtConfigurations.Value.Issuer,
                audience: jwtConfigurations.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtConfigurations.Value.ExpiryMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}