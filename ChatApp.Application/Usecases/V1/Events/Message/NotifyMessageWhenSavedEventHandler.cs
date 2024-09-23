﻿using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Constant;
using static ChatApp.Contract.Services.V1.Message.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.Message
{
    public sealed class NotifyMessageWhenSavedEventHandler : IDomainEventHandler<SavedMessageEvent>
    {
        private readonly IHubService _hubService;
        private readonly IRedisService _redisService;
        public NotifyMessageWhenSavedEventHandler(IHubService hubService, IRedisService redisService)
        {
            _hubService = hubService;
            _redisService = redisService;
        }
        public async Task Handle(SavedMessageEvent notification, CancellationToken cancellationToken)
        {
            if (notification.IsGroup)
            {
               await _hubService.NotifyTo(notification.From, notification.To, notification.IsGroup,"ReceivedMessage", notification.Message);
            }
            else
            {
                // get connection Id
                var toUser = await _redisService.GetDataByKey(KeyRedis.ListUsersOnline + notification.To);
                await _hubService.NotifyTo(notification.From, toUser, notification.IsGroup, "ReceivedMessage", notification.Message);
            }
        }
    }
}
