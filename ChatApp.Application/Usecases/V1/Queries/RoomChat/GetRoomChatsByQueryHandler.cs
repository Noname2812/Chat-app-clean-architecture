

using AutoMapper;
using ChatApp.Application.Abstractions;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Infrastructure.Dapper.Repositories;
using static ChatApp.Contract.Services.V1.RoomChat.Query;
using static ChatApp.Contract.Services.V1.RoomChat.Respone;

namespace ChatApp.Application.Usecases.V1.Queries.RoomChat
{
    public sealed class GetRoomChatsByQueryHandler :  QueryHanlderBase<Domain.Entities.RoomChat, Guid>,IQueryHandler<GetRoomChatsByQuery, List<RoomChatRespone>>
    {
        private readonly RoomChatQueryRepository _roomChatQueryRepository;
        public GetRoomChatsByQueryHandler(IRepositoryBase<Domain.Entities.RoomChat, Guid> repository, IMapper mapper, RoomChatQueryRepository roomChatQueryRepository)
            : base(repository, mapper)
        {
            _roomChatQueryRepository = roomChatQueryRepository;
        }
        public async Task<Result<List<RoomChatRespone>>> Handle(GetRoomChatsByQuery request, CancellationToken cancellationToken)
        {
            //var roomChats = await _repository.FindAll(
            //    p => p.ConversationParticipants.Any(x => x.UserId.ToString() == request.UserId)
            //    , p => p.ConversationParticipants).ToListAsync();
            //var result = _mapper.Map<List<RoomChatRespone>>(roomChats); 
            return await _roomChatQueryRepository.GetRoomChatsByUserId(Guid.Parse(request.UserId));
        }
    }
}
