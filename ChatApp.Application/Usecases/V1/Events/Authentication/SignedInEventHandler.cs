using ChatApp.Application.Abstractions.Services;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Constant;
using static ChatApp.Contract.Services.V1.Identty.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.Authentication
{
    public sealed class SignedInEventHandler 
        : IDomainEventHandler<SignedInEvent>

    {
        private readonly IRedisService _redisService;
        public SignedInEventHandler(IRedisService redisService)
        {
            _redisService = redisService;
        }
        public async Task Handle(SignedInEvent notification, CancellationToken cancellationToken)
        {
            // update cache
            await _redisService.SetData(KeyRedis.ListRefreshToken + notification.User.Id, notification.RefreshToken, TimeSpan.FromDays(1));
        }

       
    }
}
