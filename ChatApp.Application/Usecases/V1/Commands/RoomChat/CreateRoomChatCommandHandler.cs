


using ChatApp.Application.Abstractions;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.Services.V1.RoomChat;
using ChatApp.Domain.Abstractions;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Application.Usecases.V1.Commands.RoomChat
{
    public sealed class CreateRoomChatCommandHandler : CommandHandlerBase<Domain.Entities.RoomChat, Guid>, ICommandHandler<Command.CreateRoomChatCommand>
    {
        private readonly IRepositoryBase<ConversationParticipant, Guid> _conversationParticipantRepository;
        private readonly UserManager<AppUser> _userManager;
        public CreateRoomChatCommandHandler(IRepositoryBase<Domain.Entities.RoomChat, Guid> repositoryBase, IUnitOfWork unitOfWork,
            IRepositoryBase<ConversationParticipant, Guid> conversationParticipantRepository, UserManager<AppUser> userManager)
                : base(repositoryBase, unitOfWork)
        {
            _conversationParticipantRepository = conversationParticipantRepository;
            _userManager = userManager;
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
            foreach (var member in uniqueMembers)
            {
                var isUserExists = await _userManager.FindByIdAsync(member.Id.ToString());
                if (isUserExists == null)
                {
                    // throw Error User ID invalid
                    throw new RoomChatException.MemberIdIsInvalid(member.Id);
                }
                _conversationParticipantRepository.Add(new ConversationParticipant
                {
                    UserId = member.Id,
                    RoomChatId = room.Id,
                    NickName = member.NickName ?? member.Name,
                    CreatedDate = DateTime.UtcNow
                });
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
