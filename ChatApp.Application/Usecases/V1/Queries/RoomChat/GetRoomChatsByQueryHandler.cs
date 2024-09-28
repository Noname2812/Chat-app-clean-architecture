

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
    public sealed class GetRoomChatsByQueryHandler : QueryHanlderBase<Domain.Entities.RoomChat, Guid>, IQueryHandler<GetRoomChatsByQuery, PageResult<RoomChatRespone>>
    {
        public GetRoomChatsByQueryHandler(IRepositoryBase<Domain.Entities.RoomChat, Guid> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
        public async Task<Result<PageResult<RoomChatRespone>>> Handle(GetRoomChatsByQuery request, CancellationToken cancellationToken)
        {
            var PageIndex = request.PageIndex <= 0 ? PageResult<Domain.Entities.RoomChat>.DefaultPageIndex : request.PageIndex;
            var PageSize = request.PageSize <= 0
                ? PageResult<Domain.Entities.RoomChat>.DefaultPageSie
                : request.PageSize > PageResult<Domain.Entities.RoomChat>.UpperPageSize
                ? PageResult < Domain.Entities.RoomChat >.UpperPageSize : request.PageSize;
            var roomChats = await _repository.FindAll(
                p => p.ConversationParticipants.Any(x => x.UserId.ToString() == request.UserId)
                , [p => p.ConversationParticipants, p => p.Messages.OrderByDescending(m => m.CreatedDate).Take(1)])
                .OrderByDescending(r => r.ModifiedDate).Take(10)
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
            var totalCount = await _repository.GetTotalCount();
            var roomChatPageResult =  PageResult<Domain.Entities.RoomChat>.Create(roomChats ?? [], PageIndex, PageSize, totalCount);
            var result = _mapper.Map<PageResult<RoomChatRespone>>(roomChatPageResult);
            return result;
        }
    }
}
