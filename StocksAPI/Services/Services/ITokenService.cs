using Services.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        Task<string> GenerateRefreshToken(LoginUserDTO user);

        Task<string> GenerateRefreshToken(string email);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}