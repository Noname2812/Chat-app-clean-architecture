

using AutoMapper;
using ChatApp.Application.Abstractions;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static ChatApp.Contract.Services.V1.RoomChat.Query;
using static ChatApp.Contract.Services.V1.RoomChat.Respone;

namespace ChatApp.Application.Usecases.V1.Queries.RoomChat
{
    public sealed class GetRoomChatsByQueryHandler : BaseQueryHandler<Domain.Entities.RoomChat, Guid>, IQueryHandler<GetRoomChatsByQuery, PageResult<RoomChatRespone>>
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
                ? PageResult<Domain.Entities.RoomChat>.UpperPageSize : request.PageSize;
            var query = GetQuery(request);
            var roomChats = await _repository.FindAll(
                query
                , [p => p.ConversationParticipants, p => p.Messages.OrderByDescending(m => m.CreatedDate).Take(1)])
                .OrderByDescending(r => r.ModifiedDate).Take(10)
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
            var totalCount = await _repository.GetTotalCount(query);
            var roomChatPageResult = PageResult<Domain.Entities.RoomChat>.Create(roomChats ?? [], PageIndex, PageSize, totalCount);
            var result = _mapper.Map<PageResult<RoomChatRespone>>(roomChatPageResult);
            return result;
        }
        private static Expression<Func<Domain.Entities.RoomChat, bool>> GetQuery(GetRoomChatsByQuery request)
        {

            if (string.IsNullOrEmpty(request.KeySearch))
            {
                return p => p.ConversationParticipants.Any(x => x.UserId.ToString() == request.UserId);
            }
            return p => p.ConversationParticipants.Any(x => x.UserId.ToString() == request.UserId) &&
            (p.Name != null && p.Name.Contains(request.KeySearch) 
            || (p.IsGroup == false && p.ConversationParticipants.Any(x => x.NickName.Contains(request.KeySearch) && x.UserId.ToString() != request.UserId)));
        }
    }
}
