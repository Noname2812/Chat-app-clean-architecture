using ChatApp.Contract.Abstractions.Message;

namespace ChatApp.Contract.Services.V1.RoomChat
{
    public static class DomainEvent
    {
        public record RoomChatCreatedEvent(Guid Id, Guid RoomId, List<Guid> Members, bool IsGroup) : IDomainEvent;
        public record RoomChatDeletedEvent(Guid Id) : IDomainEvent;
        public record RoomChatUpdatedEvent(Guid Id) : IDomainEvent;
    }
}
