


using ChatApp.Application.Abstractions;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.Services.V1.RoomChat;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Exceptions;
using MediatR;
using static ChatApp.Contract.Services.V1.RoomChat.DomainEvent;

namespace ChatApp.Application.Usecases.V1.Commands.RoomChat
{
    public sealed class CreateRoomChatCommandHandler : CommandHandlerBase<Domain.Entities.RoomChat, Guid>, ICommandHandler<Command.CreateRoomChatCommand>
    {

        private readonly IPublisher _publisher;
        public CreateRoomChatCommandHandler(IRepositoryBase<Domain.Entities.RoomChat, Guid> repositoryBase, IPublisher publisher)
            : base(repositoryBase)
        {
            _publisher = publisher;
        }
        public async Task<Result> Handle(Command.CreateRoomChatCommand request, CancellationToken cancellationToken)
        {
            var uniqueMembers = request.Members.GroupBy(p => p.Id).Select(g => g.First()).ToList();
            if (uniqueMembers.Count < 2)
            {
                // thow error RoomChat must have at least 2 members
                throw new RoomChatException.RoomChatMustHaveAtLeastTwoMembers();
            }
            var room = new Domain.Entities.RoomChat
            {
                Id = Guid.NewGuid(),
                Avatar = request.Avatar,
                IsGroup = request.IsGroup ?? false,
                CreatedDate = DateTime.UtcNow,
            };
            if (room.IsGroup)
            {
                if (request.Members == null || request.Members.Count < 3)
                {
                    // throw error group chat must have at least 3 members.
                    throw new RoomChatException.GroupChatMustHaveAtLeastThreeMembers();
                }
                room.Name = request.Name ?? $"Group chat by {uniqueMembers.First().Name} host";
            }
            _repository.Add(room);
            // add members into conversation
            await _publisher.Publish(new RoomChatCreatedEvent(Guid.NewGuid(), room.Id, uniqueMembers), cancellationToken);
            return Result.Success();
        }
    }
}
