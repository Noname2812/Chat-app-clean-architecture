using ChatApp.Contract.Abstractions.Message;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static ChatApp.Contract.Services.V1.User.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Events.User
{
    public sealed class UpdatedNameUserEventHandler : IDomainEventHandler<UpdatedNameUserEvent>
    {
        private readonly IRepositoryBase<ConversationParticipant, Guid> _conversationParticipantRepository;
        public UpdatedNameUserEventHandler(IRepositoryBase<ConversationParticipant, Guid> repositoryBase)
        {
            _conversationParticipantRepository = repositoryBase;
        }
        public async Task Handle(UpdatedNameUserEvent notification, CancellationToken cancellationToken)
        {
            // update table ConversationParticipant
            var conversationParticipants = await _conversationParticipantRepository.FindAll(x => x.UserId == notification.UserId).ToListAsync();
            if (conversationParticipants.Any())
            {
                foreach (var participant in conversationParticipants)
                {
                    participant.NickName = notification.Name;
                    _conversationParticipantRepository.Update(participant);
                }
            }
        }
    }
}
