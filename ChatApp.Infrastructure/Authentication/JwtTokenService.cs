
using ChatApp.Infrastructure.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using ChatApp.Application.Abstractions.Services;


namespace ChatApp.Infrastructure.Authentication
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JWTOptions jwtOptions = new();
        public JwtTokenService(IConfiguration configuration)
        {
            configuration.GetSection(nameof(JWTOptions)).Bind(jwtOptions);
        }
        public string GenarateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtOptions.ExpireMin),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        public ClaimsPrincipal GetClaimsPrincipalFromExpriedToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, // on production is true
                ValidateIssuer = false, // on production is true
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null)
            {
                throw new SecurityTokenException("Invalid token !"); 
            }
            return principal;
        }

        public string GenarateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
