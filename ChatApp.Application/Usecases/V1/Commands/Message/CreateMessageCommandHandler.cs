using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Abstractions.Repositories;
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
            var room = await _roomChatRepository.FindByIdAsync(request.RoomChatId);
            if (room == null)
            {
                // throw invalid room chat 
            }
            var message = new Domain.Entities.Message
            {
                Id = request.MessageId ?? Guid.NewGuid(),
                RoomChatId = request.RoomChatId,
                Content = request.Content,
                CreatedBy = request.CreateBy,
                CreatedDate = request.CreateDate ?? DateTimeOffset.Now,
                Type = (Domain.Enums.TypeMessage)request.Type,
            };
            _messageRepository.Add(message);
            return Result.Success();
        }
    }
}
