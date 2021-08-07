using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Data;
using Services.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.UtilityServices
{
    public class TokenService : ITokenService
    {
        private readonly StockDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public TokenService(StockDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(
                    jwtSettings.GetSection("lifetime").Value));

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY")));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                audience: "stocksApi",
                claims: claims,
                expires: expiration,
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }

        public async Task<string> GenerateRefreshToken(LoginUserDTO user)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                var hash = Convert.ToBase64String(randomNumber);

                var userObj = _dbContext.AspNetUsers.First(o => o.Email == user.Email);

                userObj.RefreshToken = hash;
                userObj.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                _dbContext.SaveChanges();

                return hash;
            }
        }

        public async Task<string> GenerateRefreshToken(string email)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                var hash = Convert.ToBase64String(randomNumber);

                var userObj = _dbContext.AspNetUsers.First(o => o.Email == email);

                userObj.RefreshToken = hash;
                userObj.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                _dbContext.SaveChanges();

                return hash;
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY"))),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}