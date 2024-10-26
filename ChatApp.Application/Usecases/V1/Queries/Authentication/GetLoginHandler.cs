

using AutoMapper;
using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.DTOs;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static ChatApp.Contract.Services.V1.Identty.DomainEvent;
using static ChatApp.Contract.Services.V1.Identty.Query;
using static ChatApp.Contract.Services.V1.Identty.Respone;

namespace ChatApp.Application.Usecases.V1.Queries.Identity
{
    public sealed class GetLoginHandler : IQueryHandler<LoginQuery, LoginSuccessRespone>
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IPublisher _publisher;
        public GetLoginHandler(IJwtTokenService jwtTokenService, UserManager<AppUser> userManager, IMapper mapper, IPublisher publisher)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
            _mapper = mapper;
            _publisher = publisher;
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
                await _publisher.Publish(new SignedInEvent(Guid.NewGuid(), userexists, refreshToken), cancellationToken);
                return Result.Success(res);
            }
            throw new IdentityException.InvalidUser();
        }
    }
}
