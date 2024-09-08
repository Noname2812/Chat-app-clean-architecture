using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Infrastructure.Dapper.Repositories;
using Microsoft.AspNetCore.Identity;
using static ChatApp.Contract.Services.V1.ChatHub.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.Hub
{
    public class HubEventHandler : IDomainEventHandler<SignedInHubEvent>,
         IDomainEventHandler<SignedOutHubEvent>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRedisService _redisService;
        private readonly RoomChatQueryRepository _roomChatQueryRepository;
        private IHubService _hubService;
        public HubEventHandler(UserManager<AppUser> userManager, IRedisService redisService, 
            RoomChatQueryRepository roomChatQueryRepository, IHubService hubService)
        {
            _userManager = userManager;
            _redisService = redisService;
            _roomChatQueryRepository = roomChatQueryRepository;
            _hubService = hubService;

        }
        public async Task Handle(SignedInHubEvent notification, CancellationToken cancellationToken)
        {
            var rooms = await _roomChatQueryRepository.GetRoomChatsByUserId(Guid.Parse(notification.UserId));
            foreach (var room in rooms)
            {
                await _hubService.JoinGroupChat(notification.ConnectionId, room.Id.ToString());
            }
            await _redisService.SetData($"list-users-online:{notification.UserId}", notification.ConnectionId, TimeSpan.FromDays(1));
        }
        public async Task Handle(SignedOutHubEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(notification.UserId);
            if (user != null)
            {
                user.LastOnline = DateTimeOffset.Now;
                user.IsOnline = false;
                await Task.WhenAll(_userManager.UpdateAsync(user), _redisService.RemoveDataByKey($"list-users-online:{notification.UserId}"));
            }
        }
    }
}
