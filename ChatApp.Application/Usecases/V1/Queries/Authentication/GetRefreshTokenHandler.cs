using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Entities.Identity;
using MediatR;
using System.Security.Claims;
using static ChatApp.Contract.Services.V1.Identty.DomainEvent;
using static ChatApp.Contract.Services.V1.Identty.Query;
using static ChatApp.Contract.Services.V1.Identty.Respone;
using static ChatApp.Domain.Exceptions.IdentityException;


namespace ChatApp.Application.Usecases.V1.Queries.Identity
{
    public sealed class GetRefreshTokenHandler : IQueryHandler<RefreshTokenQuery, AuthenticatedRespone>
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRedisService _redisService;
        private readonly IPublisher _publisher;

        public GetRefreshTokenHandler(IJwtTokenService jwtTokenService, IRedisService redisService, IPublisher publisher)
        {
            _jwtTokenService = jwtTokenService;
            _redisService = redisService;
            _publisher = publisher;
        }
        public async Task<Result<AuthenticatedRespone>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var refreshTokenExists = await _redisService.GetDataByKey($"list-refresh-token:{request.UserId}");
            if (string.IsNullOrEmpty(refreshTokenExists))
            {
                throw new RefreshTokenInvalidOrExpried();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,request.UserId.ToString()),
            };
            var accessToken = _jwtTokenService.GenarateAccessToken(claims);
            var refreshToken = _jwtTokenService.GenarateRefreshToken();
            // update Redis
            await _publisher.Publish(new SignedInEvent(Guid.NewGuid(), new AppUser { Id = request.UserId}, refreshToken), cancellationToken);

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
