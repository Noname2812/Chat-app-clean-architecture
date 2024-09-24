

using AutoMapper;
using ChatApp.Application.Abstractions;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Abstractions.Repositories;
using static ChatApp.Contract.Services.V1.RoomChat.Query;
using static ChatApp.Contract.Services.V1.RoomChat.Respone;

namespace ChatApp.Application.Usecases.V1.Queries.RoomChat
{
    public sealed class GetRoomChatByIdQueryHandler : QueryHanlderBase<Domain.Entities.RoomChat, Guid>, IQueryHandler<GetRoomChatById, RoomChatRespone>
    {
        public GetRoomChatByIdQueryHandler(IRepositoryBase<Domain.Entities.RoomChat, Guid> repositoryBase, IMapper mapper) : base(repositoryBase, mapper)
        {
        }

        public async Task<Result<RoomChatRespone>> Handle(GetRoomChatById request, CancellationToken cancellationToken)
        {
            var room = await _repository.FindByIdAsync(request.Id, cancellationToken, [r => r.ConversationParticipants,
                r => r.Messages.OrderByDescending(x => x.CreatedDate).Take(request.Limit ?? 10).Skip(request.Offset ?? 0)]);
            var result = _mapper.Map<RoomChatRespone>(room);
            return result;
        }
    }
}
