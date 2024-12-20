﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebBlog.Domain.Payloads;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebBlog.Domain.Entities;

namespace WebBlog.Application.Utils
{
    public class JwtUtil
    {
        private static JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
        private static SymmetricSecurityKey GetSymmetricSecurityKey(string secretKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }

        private static SecurityToken GenerateToken(User user, string secretKey, double expirationDays)
        {
            
            var key = GetSymmetricSecurityKey(secretKey);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user_id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(expirationDays),
                SigningCredentials = creds
            };

            return _tokenHandler.CreateToken(tokenDescriptor);
        }

        public static TokenPayload GenerateAccessToken(User user, IConfiguration configuration)
        {
            var secretKey = configuration.GetSection("JwtSettings:Secret")?.Value;
            var expirationTime = double.Parse(configuration.GetSection("JwtSettings:AccessTokenExpirationTime")?.Value?? "120");
            var accessToken = GenerateToken(user, secretKey, expirationTime);
            var token = new TokenPayload();
            token.Access = _tokenHandler.WriteToken(accessToken);
            return token;
        }

        public static TokenPayload GenerateAccessAndRefreshToken(User user, IConfiguration configuration)
        {
            var secretKey = configuration.GetSection("JwtSettings:Secret")?.Value;
            var accessTokenExpirationTime = double.Parse(configuration.GetSection("JwtSettings:AccessTokenExpirationTime")?.Value ?? "120");
            var refreshTokenExpirationTime = double.Parse(configuration.GetSection("JwtSettings:RefreshTokenExpirationTime")?.Value ?? "10080");
            var accessToken = GenerateToken(user, secretKey, accessTokenExpirationTime);
            var refreshToken = GenerateToken(user, secretKey, refreshTokenExpirationTime);
            var token = new TokenPayload
            {
                Access = _tokenHandler.WriteToken(accessToken),
                Refresh = _tokenHandler.WriteToken(refreshToken)
            };
            return token;
        }
    }
}
