

using AutoMapper;
using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static ChatApp.Contract.Services.V1.Identty.Query;
using static ChatApp.Contract.Services.V1.Identty.Respone;

namespace ChatApp.Application.Usecases.V1.Queries.Identity
{
    public sealed class GetLoginHandler : IQueryHandler<LoginQuery, LoginSuccessRespone>
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRedisService _redisService;
        private readonly IMapper _mapper;
        public GetLoginHandler(IJwtTokenService jwtTokenService, UserManager<AppUser> userManager, IMapper mapper, IRedisService redisService)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
            _mapper = mapper;
            _redisService = redisService;

        }
        public async Task<Result<LoginSuccessRespone>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            // check user
            var userexists = await _userManager.FindByNameAsync(request.Email);
            if (userexists != null && await _userManager.CheckPasswordAsync(userexists, request.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userexists.Id.ToString()),
                };
                var accessToken = _jwtTokenService.GenarateAccessToken(claims);
                var refreshToken = _jwtTokenService.GenarateRefreshToken();
                var res = new LoginSuccessRespone
                {
                    Token = new AuthenticatedRespone {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        RefreshTokenExpriedtime = DateTime.Now.AddDays(1)
                    },
                    User = _mapper.Map<UserDTO>(userexists)
                };
                await _redisService.SetData($"list-refresh-token:{userexists.Id}", refreshToken, TimeSpan.FromDays(1));
                return Result.Success(res);
            }
            throw new IdentityException.InvalidUser();
        }
    }
}
