using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static ChatApp.Contract.Services.V1.Identty.Command;
using static ChatApp.Contract.Services.V1.Identty.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Commands.Identity
{
    public sealed class LogoutCommandHandler : ICommandHandler<LogoutCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IPublisher _publisher;
        public LogoutCommandHandler(UserManager<AppUser> userManager,  IJwtTokenService jwtTokenService, IPublisher publisher)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _publisher = publisher;
        }
        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var principals = _jwtTokenService.GetClaimsPrincipalFromExpriedToken(request.Token);
            var user = await _userManager.FindByIdAsync(principals.FindFirstValue(ClaimTypes.NameIdentifier));
            user.LastOnline = DateTimeOffset.Now;
            user.IsOnline = false;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _publisher.Publish(new SignedOutEvent(Guid.NewGuid(), user.Id, request.Token),cancellationToken);
                return Result.Success();
            }
            throw new Exception();
        }
    }
}
