
using System.Security.Claims;

namespace ChatApp.Application.Abstractions.Services
{
    public interface IJwtTokenService
    {
        string GenarateAccessToken(IEnumerable<Claim> claims);
        string GenarateRefreshToken();
        ClaimsPrincipal GetClaimsPrincipalFromExpriedToken(string token);
    }
}
