using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using System.Security.Claims;
using static ChatApp.Contract.Services.V1.Identty.Query;
using static ChatApp.Contract.Services.V1.Identty.Respone;


namespace ChatApp.Application.Usecases.V1.Queries.Identity
{
    public sealed class GetRefreshTokenHandler : IQueryHandler<RefreshTokenQuery, AuthenticatedRespone>
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRedisService _redisService;
        public GetRefreshTokenHandler(IJwtTokenService jwtTokenService, IRedisService redisService)
        {
            _jwtTokenService = jwtTokenService;
            _redisService = redisService;
        }
        public async Task<Result<AuthenticatedRespone>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var principals = _jwtTokenService.GetClaimsPrincipalFromExpriedToken(request.AccessToken);
            var refreshTokenExists = await _redisService.GetDataByKey($"list-refresh-token:{principals.FindFirstValue("UserId")}");
            if (string.IsNullOrEmpty(refreshTokenExists))
            {
                // throw refresh token invalid or expried time
            }
            // get claims
            var claims = new List<Claim>
            {
                new Claim("UserId",principals.FindFirstValue("UserId")),
            };
            var accessToken = _jwtTokenService.GenarateAccessToken(claims);
            var refreshToken = _jwtTokenService.GenarateRefreshToken();
            // update Redis
            await _redisService.SetData($"list-refresh-token:{principals.FindFirstValue("UserId")}", refreshToken,TimeSpan.FromMinutes(10));
            var res = new AuthenticatedRespone
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExpriedtime = DateTime.Now.AddDays(1)
            };
            return Result.Success(res);
        }
    }
}
