using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Constant;
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
             _redisService.SetData(KeyRedis.BlackListToken + notification.Token, true, TimeSpan.FromSeconds(600)),
             _redisService.RemoveDataByKey(KeyRedis.ListRefreshToken + notification.UserId));
        }
    }
}
