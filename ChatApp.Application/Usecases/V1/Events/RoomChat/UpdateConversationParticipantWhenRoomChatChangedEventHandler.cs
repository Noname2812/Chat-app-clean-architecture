

using ChatApp.Contract.Abstractions.Message;
using static ChatApp.Contract.Services.V1.RoomChat.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.RoomChat
{
    public sealed class UpdateConversationParticipantWhenRoomChatChangedEventHandler
        : IDomainEventHandler<RoomChatDeletedEvent>,
        IDomainEventHandler<RoomChatUpdatedEvent>
    {
        public Task Handle(RoomChatDeletedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Handle(RoomChatUpdatedEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
