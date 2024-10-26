

using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Constant;
using ChatApp.Contract.Services.V1.ChatHub.Common;
using static ChatApp.Contract.Services.V1.ChatHub.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.Hub
{
    public sealed class RecievedCallRequestEventHandler : IDomainEventHandler<RecievedRequestCallPrivateEvent>
    {
        private readonly IHubService _hubService;
        private readonly IRedisService _redisService;
        public RecievedCallRequestEventHandler(IHubService hubService, IRedisService redisService)
        {
            _hubService = hubService;
            _redisService = redisService;
        }
        public async Task Handle(RecievedRequestCallPrivateEvent notification, CancellationToken cancellationToken)
        {
            var to = await _redisService.GetDataObjectByKey<UserConnection>(KeyRedis.ListUsersOnline + notification.To.UserId);
            if (to != null && !to.IsCalling)
            {
                var caller = await _redisService.GetDataObjectByKey<UserConnection>(KeyRedis.ListUsersOnline + notification.Caller);
                await Task.WhenAll(_hubService.NotifyTo(to.ConnectionId, false, "IncomingCall", new
                {
                    roomChat = notification.RoomChat,
                    type = notification.Type,
                }),
                    _redisService.SetData(KeyRedis.ListUsersOnline + notification.To.UserId, new UserConnection
                    {
                        ConnectionId = to.ConnectionId,
                        IsCalling = true,
                    },null),
                     _redisService.SetData(KeyRedis.ListUsersOnline + notification.Caller, new UserConnection
                     {
                         ConnectionId = caller.ConnectionId,
                         IsCalling = true,
                     },null));
            }
            else
            {
                var userConnection = await _redisService.GetDataObjectByKey<UserConnection>(KeyRedis.ListUsersOnline + notification.Caller.ToString());
                await _hubService.NotifyTo(userConnection.ConnectionId, false, "ErrorUserIsCalling", new
                {
                    Message = $"{notification.To.NickName} is calling !"
                });

            }
        }
    }
}
