using ChatApp.Contract.Abstractions.Message;

namespace ChatApp.Contract.Services.V1.Friend
{
    public static class DomainEvent
    {
        public record CreatedRequestAddFriendEvent(Guid Id, Guid ToUser) : IDomainEvent;
        public record UpdatedStatusRequestAddFriendEvent(Guid Id, Guid From, Guid To) : IDomainEvent;
    }
}
