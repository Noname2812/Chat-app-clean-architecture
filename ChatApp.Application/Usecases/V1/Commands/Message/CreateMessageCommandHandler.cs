using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.Services.V1.RoomChat;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Enums;

using static ChatApp.Contract.Services.V1.Message.Command;


namespace ChatApp.Application.Usecases.V1.Commands.Message
{
    public sealed class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand>
    {
        private readonly IRepositoryBase<Domain.Entities.RoomChat, Guid> _roomChatRepository;
        private readonly IRepositoryBase<Domain.Entities.Message, Guid> _messageRepository;
        public CreateMessageCommandHandler(IRepositoryBase<Domain.Entities.Message, Guid> messageRepository, IRepositoryBase<Domain.Entities.RoomChat, Guid> roomChatRepository)
        {
            _roomChatRepository = roomChatRepository;
            _messageRepository = messageRepository;
        }
        public async Task<Result> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var roomId = request.RoomChatId ?? Guid.NewGuid();
            var room = await _roomChatRepository.FindByIdAsync(roomId);
            if (room == null)
            {
                return Result.Failure(new Error("Not Found", "RoomChat Invalid"));
            }
            var message = new Domain.Entities.Message
            {
                Id = request.MessageId ?? Guid.NewGuid(),
                RoomChatId = roomId,
                Content = request.Content,
                CreatedBy = request.CreateBy,
                CreatedDate = request.CreateDate ?? DateTimeOffset.Now,
                Type = request.Type ?? TypeMessage.String,
                SeenByJson = ""
            };
            _messageRepository.Add(message);
            return Result.Success();
        }
    }
}
