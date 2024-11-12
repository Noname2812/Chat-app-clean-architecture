using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Constant;
using ChatApp.Contract.Services.V1.ChatHub.Common;
using ChatApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using static ChatApp.Contract.Services.V1.ChatHub.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.Hub
{
    public sealed class HubAuthenticateEventHandler : IDomainEventHandler<SignedInHubEvent>,
         IDomainEventHandler<SignedOutHubEvent>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRedisService _redisService;
        private IHubService _hubService;
        public HubAuthenticateEventHandler(UserManager<AppUser> userManager, IRedisService redisService,
            IHubService hubService)
        {
            _userManager = userManager;
            _redisService = redisService;
            _hubService = hubService;

        }
        public async Task Handle(SignedInHubEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(notification.UserId);
            user.IsOnline = true;
            await Task.WhenAll(_userManager.UpdateAsync(user), _hubService.JoinAllGroupChatsWithUserId(notification.ConnectionId, notification.UserId),
                _redisService.SetData(KeyRedis.ListUsersOnline + notification.UserId, new UserConnection { ConnectionId = notification.ConnectionId, IsCalling = false }, TimeSpan.FromDays(1)));
        }
        public async Task Handle(SignedOutHubEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(notification.UserId);
            if (user != null)
            {
                user.LastOnline = DateTimeOffset.Now;
                user.IsOnline = false;
                await Task.WhenAll(_userManager.UpdateAsync(user), _redisService.RemoveDataByKey(KeyRedis.ListUsersOnline + notification.UserId));
            }
        }
    }
}
