

using AutoMapper;
using ChatApp.Application.Abstractions;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using static ChatApp.Contract.Services.V1.RoomChat.Query;
using static ChatApp.Contract.Services.V1.RoomChat.Respone;

namespace ChatApp.Application.Usecases.V1.Queries.RoomChat
{
    public sealed class GetRoomChatsByQueryHandler : QueryHanlderBase<Domain.Entities.RoomChat, Guid>, IQueryHandler<GetRoomChatsByQuery, List<RoomChatRespone>>
    {
        public GetRoomChatsByQueryHandler(IRepositoryBase<Domain.Entities.RoomChat, Guid> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
        public async Task<Result<List<RoomChatRespone>>> Handle(GetRoomChatsByQuery request, CancellationToken cancellationToken)
        {
            var roomChats = await _repository.FindAll(
                p => p.ConversationParticipants.Any(x => x.UserId.ToString() == request.UserId)
                , [p => p.ConversationParticipants, p => p.Messages.OrderByDescending(m => m.CreatedDate).Take(10)])
                .OrderByDescending(r => r.ModifiedDate).Take(10)
                .ToListAsync();
            var result = _mapper.Map<List<RoomChatRespone>>(roomChats);
            return result;
        }
    }
}
