using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Constant;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Entities;
using static ChatApp.Contract.Services.V1.Message.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.Message
{
    public sealed class NotifyMessageWhenSavedEventHandler : IDomainEventHandler<SavedMessageEvent>
    {
        private readonly IHubService _hubService;
        private readonly IRedisService _redisService;
        private readonly IRepositoryBase<ConversationParticipant, Guid> _repository;
        public NotifyMessageWhenSavedEventHandler(IHubService hubService, IRedisService redisService, IRepositoryBase<ConversationParticipant, Guid> repository)
        {
            _hubService = hubService;
            _redisService = redisService;
            _repository = repository;
        }
        public async Task Handle(SavedMessageEvent notification, CancellationToken cancellationToken)
        {
            if (notification.IsGroup)
            {
               await _hubService.NotifyTo( notification.To, notification.IsGroup,"ReceivedMessage", notification.Message);
            }
            else
            {
                var user = await _repository.FindSingleAsync(x => x.RoomChatId == Guid.Parse(notification.To) 
                && x.UserId != Guid.Parse(notification.From));
                // get connection Id
                var toUser = await _redisService.GetDataByKey(KeyRedis.ListUsersOnline + user.UserId);
                if(toUser != null) { 

                await _hubService.NotifyTo( toUser, notification.IsGroup, "ReceivedMessage", notification.Message);
                }
            }
        }
    }
}
