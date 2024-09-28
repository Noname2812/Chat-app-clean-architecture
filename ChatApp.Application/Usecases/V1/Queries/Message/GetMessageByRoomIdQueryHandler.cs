using AutoMapper;
using ChatApp.Application.Abstractions;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.DTOs;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static ChatApp.Contract.Services.V1.Message.Query;
using static ChatApp.Contract.Services.V1.RoomChat.Respone;

namespace ChatApp.Application.Usecases.V1.Queries.Message
{
    internal class GetMessageByRoomIdQueryHandler :QueryHanlderBase<Domain.Entities.Message,Guid>, IQueryHandler<GetMessagesByRoomIdQuery, PageResult<MessageDTO>>
    {
        public GetMessageByRoomIdQueryHandler(IRepositoryBase<Domain.Entities.Message, Guid> repositoryBase, IMapper mapper) : base(repositoryBase, mapper)
        {
        }

        public async Task<Result<PageResult<MessageDTO>>> Handle(GetMessagesByRoomIdQuery request, CancellationToken cancellationToken)
        {
            var PageIndex = request.PageIndex <= 0 ? PageResult<Domain.Entities.Message>.DefaultPageIndex : request.PageIndex;
            var PageSize = request.PageSize <= 0
                ? PageResult<Domain.Entities.Message>.DefaultPageSie
                : request.PageSize > PageResult<Domain.Entities.Message>.UpperPageSize
                ? PageResult<Domain.Entities.Message>.UpperPageSize : request.PageSize;
            var messages = await _repository.FindAll(
                m => m.RoomChatId == request.RoomId,
                [])
                .OrderByDescending(r => r.ModifiedDate)
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
            var msgs = await _repository.FindAll(x => x.RoomChatId == request.RoomId, [x => x.RoomChat]).Take(10).Skip((PageIndex - 1) * PageSize).ToListAsync();
            var totalCount = await _repository.GetTotalCount();
            var messagePageResult = PageResult<Domain.Entities.Message>.Create(messages ?? [], PageIndex, PageSize, totalCount);
            var result = _mapper.Map<PageResult<MessageDTO>>(messagePageResult);
            return result;
        }
    }
}
