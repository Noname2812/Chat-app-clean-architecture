﻿using ChatApp.Contract.Abstractions.Message;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static ChatApp.Contract.Services.V1.ConversationParticipant.DomainEvent;
using static ChatApp.Contract.Services.V1.RoomChat.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.RoomChat
{
    public sealed class CreateConversationParticipantWhenRoomChatCreatedEventHandler
        : IDomainEventHandler<RoomChatCreatedEvent>
    {
        private readonly IRepositoryBase<ConversationParticipant, Guid> _conversationParticipantRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPublisher _publisher;
        public CreateConversationParticipantWhenRoomChatCreatedEventHandler(IRepositoryBase<ConversationParticipant, Guid> conversationParticipantRepository,
            UserManager<AppUser> userManager, IPublisher publisher)
        {
            _conversationParticipantRepository = conversationParticipantRepository;
            _userManager = userManager;
            _publisher = publisher;
        }
        public async Task Handle(RoomChatCreatedEvent notification, CancellationToken cancellationToken)
        {
            foreach (var member in notification.Members)
            {

                var userExists = await _userManager.FindByIdAsync(member.ToString());
                if (userExists == null)
                {
                    // throw Error User ID invalid
                    throw new IdentityException.UserNotFound(member);
                }
                _conversationParticipantRepository.Add(new ConversationParticipant
                {
                    UserId = member,
                    RoomChatId = notification.RoomId,
                    NickName = userExists.Name,
                    CreatedDate = DateTime.UtcNow
                });
                if (notification.IsGroup)
                {
                    await _publisher.Publish(new AddMemberIntoGroupHub(Guid.NewGuid(), member, notification.RoomId));
                }
            }
        }
    }
}
