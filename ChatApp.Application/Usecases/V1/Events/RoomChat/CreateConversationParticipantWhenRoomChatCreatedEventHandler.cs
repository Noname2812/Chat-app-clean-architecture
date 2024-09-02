using ChatApp.Contract.Abstractions.Message;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using static ChatApp.Contract.Services.V1.RoomChat.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.RoomChat
{
    public sealed class CreateConversationParticipantWhenRoomChatCreatedEventHandler
        : IDomainEventHandler<RoomChatCreatedEvent>
    {
        private readonly IRepositoryBase<ConversationParticipant, Guid> _conversationParticipantRepository;
        private readonly UserManager<AppUser> _userManager;
        public CreateConversationParticipantWhenRoomChatCreatedEventHandler(IRepositoryBase<ConversationParticipant, Guid> conversationParticipantRepository, UserManager<AppUser> userManager)
        {
            _conversationParticipantRepository = conversationParticipantRepository;
            _userManager = userManager;
        }
        public async Task Handle(RoomChatCreatedEvent notification, CancellationToken cancellationToken)
        {
            foreach (var member in notification.Members)
            {
                var isUserExists = await _userManager.FindByIdAsync(member.Id.ToString());
                if (isUserExists == null)
                {
                    // throw Error User ID invalid
                    throw new IdentityException.UserNotFound(member.Id);
                }
                _conversationParticipantRepository.Add(new ConversationParticipant
                {
                    UserId = member.Id,
                    RoomChatId = notification.RoomId,
                    NickName = member.NickName ?? member.Name,
                    CreatedDate = DateTime.UtcNow
                });
            }
        }
    }
}
