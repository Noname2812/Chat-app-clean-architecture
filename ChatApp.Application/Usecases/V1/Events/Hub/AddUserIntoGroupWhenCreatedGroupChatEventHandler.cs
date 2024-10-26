using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Constant;
using ChatApp.Contract.Services.V1.ChatHub.Common;
using static ChatApp.Contract.Services.V1.ConversationParticipant.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.Hub
{
    public sealed class AddUserIntoGroupWhenCreatedGroupChatEventHandler : IDomainEventHandler<AddMemberIntoGroupHub>
    {
        private readonly IRedisService _redisService;
        private readonly IHubService _hubService;
        public AddUserIntoGroupWhenCreatedGroupChatEventHandler( IRedisService redisService, IHubService hubService)
        {
            _hubService = hubService;
            _redisService = redisService;
        }

        public async Task Handle(AddMemberIntoGroupHub notification, CancellationToken cancellationToken)
        {
            var userConnection = await _redisService.GetDataObjectByKey<UserConnection>(KeyRedis.ListUsersOnline + notification.UserId);
            if (userConnection != null)
            {
                await _hubService.AddMemberIntoGroup(userConnection.ConnectionId, notification.RoomchatId.ToString());
            }
        }
    }
}
