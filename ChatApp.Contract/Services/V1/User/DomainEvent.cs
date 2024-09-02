using ChatApp.Contract.Abstractions.Message;

namespace ChatApp.Contract.Services.V1.User
{
    public static class DomainEvent
    {
        public record UpdatedNameUserEvent(Guid Id, Guid UserId, string Name) : IDomainEvent;
    }
}
