using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static ChatApp.Contract.Services.V1.Identty.Command;

namespace ChatApp.Application.Usecases.V1.Commands.Identity
{
    public sealed class LogoutCommandHandler : ICommandHandler<LogoutCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRedisService _redisService;
        public LogoutCommandHandler(UserManager<AppUser> userManager, IRedisService redisService, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _redisService = redisService;
            _jwtTokenService = jwtTokenService;

        }
        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var principals = _jwtTokenService.GetClaimsPrincipalFromExpriedToken(request.Token);
            var user = await _userManager.FindByIdAsync(principals.FindFirstValue(ClaimTypes.NameIdentifier));
            user.LastOnline = DateTimeOffset.Now;
            user.IsOnline = false;
            var result = await _userManager.UpdateAsync(user);
            if(result.Succeeded)
            {
                await _redisService.SetData($"black-list-token:{request.Token}", true, TimeSpan.FromSeconds(600));
                await _redisService.RemoveDataByKey($"list-refresh-token:{user.Id}");
                return Result.Success();
            }
            throw new Exception();
        }
    }
}
