

using ChatApp.Contract.Abstractions.Message;

namespace ChatApp.Contract.Services.V1.ConversationParticipant
{
    public static class DomainEvent
    {
        public record AddMemberIntoGroupHub(Guid Id, Guid UserId, Guid RoomchatId) : IDomainEvent;
    }
}
