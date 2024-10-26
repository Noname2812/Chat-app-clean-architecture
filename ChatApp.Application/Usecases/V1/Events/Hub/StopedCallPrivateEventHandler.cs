﻿

using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Constant;
using ChatApp.Contract.Services.V1.ChatHub.Common;
using static ChatApp.Contract.Services.V1.ChatHub.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.Hub
{
    public sealed class StopedCallPrivateEventHandler : IDomainEventHandler<StopedCallEvent>
    {
        private readonly IHubService _hubService;
        private readonly IRedisService _redisService;
        public StopedCallPrivateEventHandler(IHubService hubService, IRedisService redisService)
        {
            _hubService = hubService;
            _redisService = redisService;
        }
        public async Task Handle(StopedCallEvent notification, CancellationToken cancellationToken)
        {
            // update cache
            var caller = await _redisService.GetDataObjectByKey<UserConnection>(KeyRedis.ListUsersOnline + notification.Caller.ToString());
            caller.IsCalling = false;
            if (notification.IsGroup)
            {
                // stop caller 
            }
            else
            {
                // stop private call
                var to = await _redisService.GetDataObjectByKey<UserConnection>(KeyRedis.ListUsersOnline + notification.To.ToString());
                to.IsCalling = false;
                await Task.WhenAll(_hubService.NotifyTo(to.ConnectionId, false, "EndCall", 1),
                    _redisService.SetData(KeyRedis.ListUsersOnline + notification.Caller, caller, null),
                    _redisService.SetData(KeyRedis.ListUsersOnline + notification.To, to, null));
            }
        }
    }
}
