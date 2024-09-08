using ChatApp.Contract.Abstractions.Message;

namespace ChatApp.Contract.Services.V1.ChatHub
{
    public static class DomainEvent
    {
        public record SignedInHubEvent(Guid Id,string UserId, string ConnectionId) : IDomainEvent;
        public record SignedOutHubEvent(Guid Id,string UserId) : IDomainEvent;
    }
}
