using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hospital_Hub_Portal.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hospital_Hub_Portal.Services
{
    public class TokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        #region CreateAccessToken
        public string CreateAccessToken(HhUser user)
        {
            var roleValue = string.IsNullOrWhiteSpace(user.UserRole) ? "User" : user.UserRole;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserEmail ?? string.Empty),
                new Claim(ClaimTypes.Role, roleValue),
                new Claim("role", roleValue)
            };

            // ✅ FIXED: convert hex string to bytes
            var key = new SymmetricSecurityKey(Convert.FromHexString(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
