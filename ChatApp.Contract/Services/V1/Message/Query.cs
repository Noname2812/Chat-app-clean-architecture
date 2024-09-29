

using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.DTOs;

namespace ChatApp.Contract.Services.V1.Message
{
    public static class Query
    {
        public record GetMessagesByRoomIdQuery(Guid RoomId,string? KeySearch, int PageSize, int PageIndex) : IQuery<PageResult<MessageDTO>>;
    }
}
