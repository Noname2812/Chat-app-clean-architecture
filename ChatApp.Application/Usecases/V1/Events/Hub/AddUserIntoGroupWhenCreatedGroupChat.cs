using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Constant;
using static ChatApp.Contract.Services.V1.ConversationParticipant.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.Hub
{
    public class AddUserIntoGroupWhenCreatedGroupChat : IDomainEventHandler<AddMemberIntoGroupHub>
    {
        private readonly IRedisService _redisService;
        private readonly IHubService _hubService;
        public AddUserIntoGroupWhenCreatedGroupChat( IRedisService redisService, IHubService hubService)
        {
            _hubService = hubService;
            _redisService = redisService;
        }

        public async Task Handle(AddMemberIntoGroupHub notification, CancellationToken cancellationToken)
        {
            var conenctionId = await _redisService.GetDataByKey(KeyRedis.ListUsersOnline + notification.UserId);
            if (conenctionId != null)
            {
                await _hubService.AddMemberIntoGroup(conenctionId, notification.RoomchatId.ToString());
            }
        }
    }
}
