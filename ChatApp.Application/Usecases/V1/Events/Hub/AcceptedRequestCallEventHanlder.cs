using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Constant;
using ChatApp.Contract.Services.V1.ChatHub.Common;
using static ChatApp.Contract.Services.V1.ChatHub.DomainEvent;
namespace ChatApp.Application.Usecases.V1.Events.Hub
{
    public sealed class AcceptedRequestCallEventHanlder : IDomainEventHandler<AcceptedRequestCallEvent>
    {
        private readonly IRedisService _redisService;
        private readonly IHubService _hubService;
        public AcceptedRequestCallEventHanlder(IRedisService redisService, IHubService hubService)
        {
            _hubService = hubService;
            _redisService = redisService;
        }
        public async Task Handle(AcceptedRequestCallEvent notification, CancellationToken cancellationToken)
        {
            if (!notification.RoomChat.IsGroup)
            {
                var from = notification.RoomChat.ConversationParticipants.Where(x => x.UserId != notification.Self).FirstOrDefault();
                if (from != null)
                {
                    var userConnection = await _redisService.GetDataObjectByKey<UserConnection>(KeyRedis.ListUsersOnline + from.UserId);
                    if (userConnection != null)
                    {
                        var token = _hubService.GenarateTokenZegoClould(notification.Params.AppId, from.UserId.ToString(), notification.Params.Secret,
             notification.Params.EffectiveTimeInSeconds, notification.Params.Payload);
                        await _hubService.NotifyTo(userConnection.ConnectionId, false, "RecievedTokenZegoCloud", token);
                    }
                }
            }
        }
    }
}
