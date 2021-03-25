using Microsoft.IdentityModel.Tokens;
using MysteryShop.Infrastructure.DTOs;
using MysteryShop.Infrastructure.Extensions;
using MysteryShop.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MysteryShop.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _settings;

        public JwtHandler(JwtOptions settings)
        {
            _settings = settings;
        }

        public JwtDTO CreateToken(Guid userId)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString(), ClaimValueTypes.Integer64)
            };

            var expires = now.AddMinutes(_settings.ExpiryMinutes);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)),
                SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: _settings.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDTO
            {
                UserId = userId,
                AccessToken = token,
                Expires = expires.ToTimestamp()
            };

        }
    }
}
