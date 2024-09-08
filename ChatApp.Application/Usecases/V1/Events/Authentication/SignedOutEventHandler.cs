using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using static ChatApp.Contract.Services.V1.ChatHub.DomainEvent;
using static ChatApp.Contract.Services.V1.Identty.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.Authentication
{
    public sealed class SignedOutEventHandler 
        : IDomainEventHandler<SignedOutEvent>
    {
        private readonly IRedisService _redisService;
        public SignedOutEventHandler(IRedisService redisService)
        {
            _redisService = redisService;
        }
        public async Task Handle(SignedOutEvent notification, CancellationToken cancellationToken)
        {
            // update cache
            await Task.WhenAll(
             _redisService.SetData($"black-list-token:{notification.Token}", true, TimeSpan.FromSeconds(600)),
             _redisService.RemoveDataByKey($"list-refresh-token:{notification.UserId}"));
        }
    }
}
